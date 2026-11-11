using BelanjaYuk.Dtos.Auth;
using BelanjaYuk.Entities;
using BelanjaYuk.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BelanjaYuk.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly AppDbContext _context;
		public AuthController(AppDbContext context)
		{
			_context = context;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequest req)
		{
			// Validasi Nama Lengkap
			if (string.IsNullOrWhiteSpace(req.FullName))
				return BadRequest(new { message = "Nama wajib diisi." });

			if (!Regex.IsMatch(req.FullName, @"^[A-Za-z\s]+$"))
				return BadRequest(new { message = "Nama hanya boleh huruf dan spasi." });

			// Validasi Username 
			if (string.IsNullOrWhiteSpace(req.UserName))
				return BadRequest(new { message = "Username wajib diisi." });

			if (req.UserName.Length < 5 || req.UserName.Length > 30)
				return BadRequest(new { message = "Username harus 5–30 karakter." });

			if (!Regex.IsMatch(req.UserName, @"^[a-z0-9_]+$"))
				return BadRequest(new { message = "Username hanya boleh huruf kecil, angka, dan underscore (_)." });

			// Validasi Email 
			if (string.IsNullOrWhiteSpace(req.Email))
				return BadRequest(new { message = "Email wajib diisi." });

			if (!Regex.IsMatch(req.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
				return BadRequest(new { message = "Format email tidak valid." });

			// Validasi Nomor HP
			if (string.IsNullOrWhiteSpace(req.PhoneNumber))
				return BadRequest(new { message = "Nomor HP wajib diisi." });

			if (!Regex.IsMatch(req.PhoneNumber, @"^(08|\+628)[0-9]{8,11}$"))
				return BadRequest(new { message = "Nomor HP tidak valid. Gunakan format 08 atau +628, 10–13 digit angka." });

			// Validasi Password
			if (string.IsNullOrWhiteSpace(req.Password))
				return BadRequest(new { message = "Kata sandi wajib diisi." });

			if (req.Password.Length < 8)
				return BadRequest(new { message = "Kata sandi minimal 8 karakter." });

			if (!Regex.IsMatch(req.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"))
				return BadRequest(new { message = "Kata sandi harus kombinasi huruf besar, kecil, dan angka." });

			// Validasi Konfirmasi Sandi
			if (req.Password != req.ConfirmPassword)
				return BadRequest(new { message = "Kata sandi tidak sama." });

			// Validasi Tanggal Lahir
			if (req.DOB == DateTime.MinValue)
				return BadRequest(new { message = "Tanggal lahir wajib diisi." });

			if (req.DOB > DateTime.Today)
				return BadRequest(new { message = "Tanggal lahir tidak boleh lebih dari hari ini." });

			var age = DateTime.Today.Year - req.DOB.Year;
			if (req.DOB > DateTime.Today.AddYears(-age)) age--;
			if (age < 13)
				return BadRequest(new { message = "Minimal umur 13 tahun." });

			// Validasi Jenis Kelamin
			if (string.IsNullOrWhiteSpace(req.IdGender))
				return BadRequest(new { message = "Jenis kelamin wajib diisi." });

			var genderExists = await _context.LtGenders.AnyAsync(g => g.IdGender == req.IdGender && g.IsActive);
			if (!genderExists)
				return BadRequest(new { message = "Jenis kelamin tidak valid (tidak ada di database)." });

			// Validasi Unik
			if (await _context.MsUsers.AnyAsync(u => u.UserName == req.UserName))
				return BadRequest(new { message = "Username sudah digunakan." });

			if (await _context.MsUsers.AnyAsync(u => u.Email == req.Email))
				return BadRequest(new { message = "Email sudah terdaftar." });

			if (await _context.MsUsers.AnyAsync(u => u.PhoneNumber == req.PhoneNumber))
				return BadRequest(new { message = "Nomor HP sudah terdaftar." });

			// Validasi Alamat Utama (opsional)
			bool alamatDiisi = !string.IsNullOrWhiteSpace(req.Provinsi)
							|| !string.IsNullOrWhiteSpace(req.KotaKabupaten)
							|| !string.IsNullOrWhiteSpace(req.Kecamatan)
							|| !string.IsNullOrWhiteSpace(req.KodePos)
							|| !string.IsNullOrWhiteSpace(req.HomeAddressDesc);

			if (alamatDiisi)
			{
				if (string.IsNullOrWhiteSpace(req.Provinsi))
					return BadRequest(new { message = "Provinsi wajib diisi." });

				if (string.IsNullOrWhiteSpace(req.KotaKabupaten))
					return BadRequest(new { message = "Kota/Kabupaten wajib diisi." });

				if (string.IsNullOrWhiteSpace(req.Kecamatan))
					return BadRequest(new { message = "Kecamatan wajib diisi." });

				if (string.IsNullOrWhiteSpace(req.HomeAddressDesc) || req.HomeAddressDesc.Length < 10)
					return BadRequest(new { message = "Alamat minimal 10 karakter." });

				if (!Regex.IsMatch(req.KodePos ?? "", @"^\d{5}$"))
					return BadRequest(new { message = "Kode pos tidak valid. Harus 5 digit angka." });
			}

			// Simpan User Baru 
			var newUser = new MsUser
			{
				IdUser = Guid.NewGuid().ToString(),
				UserName = req.UserName,
				Email = req.Email,
				PhoneNumber = req.PhoneNumber,
				FirstName = req.FullName,
				DOB = req.DOB,
				IdGender = req.IdGender,
				IsActive = true,
				DateIn = DateTime.Now
			};

			// Hash password
			var passwordHash = ComputeSha256Hash(req.Password);

			var userPassword = new MsUserPassword
			{
				IdUserPassword = Guid.NewGuid().ToString(),
				IdUser = newUser.IdUser,
				PasswordHash = passwordHash,
				IsActive = true,
				DateIn = DateTime.Now
			};

			_context.MsUsers.Add(newUser);
			_context.MsUserPasswords.Add(userPassword);

			// Jika alamat utama diisi -> simpan juga
			if (alamatDiisi)
			{
				var address = new TrHomeAddress
				{
					IdHomeAddress = Guid.NewGuid().ToString(),
					IdUser = newUser.IdUser,
					Provinsi = req.Provinsi,
					KotaKabupaten = req.KotaKabupaten,
					Kecamatan = req.Kecamatan,
					KodePos = req.KodePos,
					HomeAddressDesc = req.HomeAddressDesc,
					IsPrimaryAddress = true,
					IsActive = true,
					DateIn = DateTime.Now
				};
				_context.TrHomeAddresses.Add(address);
			}

			await _context.SaveChangesAsync();

			return Ok(new RegisterResponse
			{
				IdUser = newUser.IdUser,
				UserName = newUser.UserName,
				Email = newUser.Email,
				Message = "Registrasi berhasil. Silakan login."
			});
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.EmailOrPhone))
				return BadRequest(new { message = "Email atau nomor HP wajib diisi." });

			if (string.IsNullOrWhiteSpace(request.Password))
				return BadRequest(new { message = "Kata sandi wajib diisi." });

			// Cari user berdasarkan email atau phone
			var user = await _context.MsUsers
				.FirstOrDefaultAsync(u =>
					u.Email == request.EmailOrPhone ||
					u.PhoneNumber == request.EmailOrPhone);

			if (user == null)
				return NotFound(new { message = "Email atau nomor HP tidak terdaftar." });

			// Ambil password hash user
			var userPassword = await _context.MsUserPasswords
				.FirstOrDefaultAsync(p => p.IdUser == user.IdUser && p.IsActive);

			if (userPassword == null)
				return Unauthorized(new { message = "Kata sandi salah." });

			// Hash password input untuk dibandingkan
			var inputHash = ComputeSha256Hash(request.Password);

			if (userPassword.PasswordHash != inputHash)
				return Unauthorized(new { message = "Kata sandi salah." });

			var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

			return Ok(new LoginResponse
			{
				IdUser = user.IdUser,
				UserName = user.UserName,
				Email = user.Email,
				Token = token
			});
		}

		private static string ComputeSha256Hash(string rawData)
		{
			using var sha256 = SHA256.Create();
			var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
			var builder = new StringBuilder();
			foreach (var b in bytes)
				builder.Append(b.ToString("x2"));
			return builder.ToString();
		}
	}
}
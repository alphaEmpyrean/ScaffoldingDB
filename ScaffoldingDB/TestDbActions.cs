using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScaffoldingDB.Data;
using ScaffoldingDB.Entities;

namespace ScaffoldingDB
{
    public class TestDbActions
    {
        private readonly RediRndContext _context;
        public TestDbActions(RediRndContext context) 
        {
            _context = context;
        }

        public void Run()
        {
            var testUser = _context.Stakers.First();

            var passwordHasher = new PasswordHasher<Staker>();
            var hashedPassword = passwordHasher.HashPassword(testUser, "Password");
            var hashedPassword1 = passwordHasher.HashPassword(testUser, "ReallyLongStringToSeeHowItChanges");
            var hashedPassword2 = passwordHasher.HashPassword(testUser, "ReallyLongStringToSeeHowItChangesReallyLongStringToSeeHowItChanges");

            System.Diagnostics.Debug.WriteLine($"Hashed Password Length: {hashedPassword.Length}");
            System.Diagnostics.Debug.WriteLine($"Hashed Password Length: {hashedPassword1.Length}");
            System.Diagnostics.Debug.WriteLine($"Hashed Password Length: {hashedPassword2.Length}");

            hashedPassword.Length
        }
    }
}

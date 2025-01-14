﻿namespace ClothesStore.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public virtual Address Address { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}

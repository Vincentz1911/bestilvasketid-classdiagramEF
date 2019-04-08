using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassDiagram.Models
{
    public class EntityDB : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = 192.168.4.224; Initial Catalog = TestDB2; Persist Security Info = True; User ID = hepo; Password = ***********");
        }
        //public DbSet<DBDefaults> DBDefaults { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<EndUser> EndUser { get; set; }
        public DbSet<AdminUser> AdminUser { get; set; }
        public DbSet<Laundry> Laundry { get; set; }
        public DbSet<Machine> Machine { get; set; }
        public DbSet<Schedules> Schedules { get; set; }
    }

    public class DBDefaults
    {
        public int ID { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deleted { get; set; }
    }

    public class Person
    {
        [Key]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
    }

    public class EndUser : DBDefaults
    {
        public Person PersonData { get; set; }
        public Address Address { get; set; }
    }

    public class AdminUser : DBDefaults
    {
        public Person PersonData { get; set; }
        public bool CreateDeleteMachine { get; set; }
        public bool ChangeDuration { get; set; }
        public bool ChangeOpeningHours { get; set; }
        public bool DeleteEndUsers { get; set; }
        public bool DeleteAdmins { get; set; }
        public bool ChangeShowID { get; set; }
        public bool ChangeScheduleLimit { get; set; }
        public bool IsMaster { get; set; }
        //En adminUser kan have flere vaskerier, men et vaskeri kan godt have flere adminUsers
        List<Laundry> Laundry = new List<Laundry>(); 
    }

    public enum ShowIDEnum { Anonym, Adresse, Navn }

    public class Laundry : DBDefaults
    {
        public string Name { get; set; }
        public int Pin { get; set; }
        public ShowIDEnum ShowID { get; set; }
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }
        public int ScheduleLimit { get; set; }
        public int DefaultDuration { get; set; }
        List<Address> Addresses = new List<Address>();
        List<Machine> Machines = new List<Machine>();
    }

    public enum MachineStatusEnum { OK, OutOfOrder, TechnicianCalled }

    public class Machine : DBDefaults
    {
        public string Name { get; set; }
        public string  Description { get; set; }
        public MachineStatusEnum Status { get; set; }
        public int? MachineDuration { get; set; }
        List<Schedules> ScheduleList = new List<Schedules>();
    }

    public enum ScheduleStatusEnum { Free, Booked, OutOfOrder }

    public class Schedules : DBDefaults
    {
        public DateTime Time { get; set; }
        public EndUser EndUser { get; set; }
        public ScheduleStatusEnum Status { get; set; }
    }

    public class Address : DBDefaults
    {
        public string Streetname { get; set; }
        public string HouseNumber { get; set; }
        public string Floor { get; set; }
        public string Door { get; set; }
        public int Zipcode { get; set; }
        public string City { get; set; }
    }
}



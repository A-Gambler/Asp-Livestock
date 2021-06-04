using System;
using System.Collections.Generic;
using System.Text;
using livestock.Models;
using LiveStok.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace livestock.Data
{
    //public class ApplicationDbContext : IdentityDbContext
    public class ApplicationDbContext : IdentityDbContext<LiveStok.Helpers.ApplicationUser>

    {
        public DbSet<StockPurchase> StockPurchase { get; set; }
        public DbSet<Buyer> Buyer { get; set; }
        public DbSet<TypeOfAnimal> TypeOfAnimals { get; set; }
        public DbSet<Agent> Agent { get; set; }
        public DbSet<Location> Locationts { get; set; }
        public DbSet<Vendor> Vendor { get; set; }
        public DbSet<Transport> Transport { get; set; }
        public DbSet<Weight> Weights { get; set; }
        //public DbSet<PriceXWeight> PriceXWeights { get; set; }
        public DbSet<PricePerHeadBuy> PricePerHeadBuy { get; set; }
        public DbSet<UserOpenInvitation> UserOpenInvitations { get; set; }
        public DbSet<MarketBuy> MarketBuys { get; set; }
        public DbSet<MarketBuySummary> MarketBuySummaries { get; set; }
        public DbSet<BuyType> BuyTypes { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StockPurchase>()
                .HasOne(p => p.Buyer)
                .WithMany(b => b.StockPurchase)
                .HasForeignKey(d=> d.BuyerID)
                .HasConstraintName("FK_StockPurchase_Buyer");

            modelBuilder.Entity<StockPurchase>()
                .HasOne(p => p.TypeOfAnimal)
                .WithMany(b => b.StockPurchase)
                .HasForeignKey(d => d.TypeOfAnimalID)
                .HasConstraintName("FK_StockPurchase_TypeOfAnimal");

            modelBuilder.Entity<StockPurchase>()
                .HasOne(p => p.Agent)
                .WithMany(b => b.StockPurchase)
                .HasForeignKey(d => d.AgentID)
                .HasConstraintName("FK_StockPurchase_Agent");

            modelBuilder.Entity<StockPurchase>()
                .HasOne(p => p.Location)
                .WithMany(b => b.StockPurchase)
                .HasForeignKey(d => d.LocationID)
                .HasConstraintName("FK_StockPurchase_Location");

            //modelBuilder.Entity<StockPurchase>()
            //    .HasOne(p => p.Vendor)
            //    .WithMany(b => b.StockPurchase)
            //    .HasForeignKey(d => d.VendorID)
            //    .HasConstraintName("FK_StockPurchase_Vendor");

            modelBuilder.Entity<StockPurchase>()
                .HasOne(p => p.Transport)
                .WithMany(b => b.StockPurchase)
                .HasForeignKey(d => d.TransportID)
                .HasConstraintName("FK_StockPurchase_Transport");

            modelBuilder.Entity<StockPurchase>()
                .HasOne(p => p.WeightSheet)
                .WithOne(b => b.StockPurchase)
                .HasConstraintName("FK_StockPurchase_WeightSheet");

            modelBuilder.Entity<Weight>()
                .HasOne(p => p.weightSheet)
                .WithMany(b => b.Weights)
                .HasForeignKey(d => d.WeightSheetID)
                .HasConstraintName("FK_Weight_WeightSheet");

            modelBuilder.Entity<PricePerHeadBuy>()
                .HasOne(p => p.TypeOfAnimal)
                .WithMany(b => b.PricePerHeadBuys)
                .HasForeignKey(d => d.TypeOfAnimalID)
                .HasConstraintName("FK_PricePerHeadBuy_TypeOfAnimal");


            modelBuilder.Entity<PricePerHeadBuy>()
                .HasOne(p => p.StockPurchase)
                .WithMany(b => b.PricePerHeadBuys)
                .HasForeignKey(d => d.StockPurchaseID)
                .HasConstraintName("FK_PricePerHeadBuy_StockPurchase");

            modelBuilder.Entity<MarketBuy>()
             .HasOne(p => p.StockPurchase)
             .WithMany(b => b.MarketBuys)
             .HasForeignKey(d => d.StockPurchaseID)
             .HasConstraintName("FK_MarketBuy_StockPurchase");

            modelBuilder.Entity<MarketBuySummary>()
                .HasOne(p => p.stockPurchase)
                .WithMany(b => b.MarketBuySummaries)
                .HasForeignKey(d => d.StockPurchaseID)
                .HasConstraintName("FK_MarketBuySummary_StockPurchase");

            //modelBuilder.Entity<StockPurchase>()
            //    .HasOne(p => p.buyType)
            //    .WithMany(b => b.StockPurchases)
            //    .HasForeignKey(d => d.BuyTypeID)
            //    .HasConstraintName("FK_StockPurchase_BuyType2");



            //modelBuilder.Entity<PriceXWeight>()
            //    .HasOne(p => p.TypeOfAnimal)
            //    .WithMany(b => b.PriceXWeights)
            //    .HasForeignKey(d => d.TypeOfAnimalID)
            //    .HasConstraintName("FK_PriceXWeight_TypeOfAnimal");


            //modelBuilder.Entity<PricePerHeadBuy>()
            //    .HasOne(p => p.StockPurchase)
            //    .WithOne(b => b.PricePerHeadBuy)
            //    .HasConstraintName("FK_StockPurchase_PricePerHeadBuy");


            //modelBuilder.Entity<TypeOfAnimal>()
            //    .HasOne(p => p.PricePerHeadBuy)
            //    .WithOne(b => b.TypeOfAnimal)
            //    .HasConstraintName("FK_TypeOfAnimal_PricePerHeadBuy");



            modelBuilder.Entity<TypeOfAnimal>().HasData(new TypeOfAnimal { ID = Guid.Parse("1550b1b3-43aa-4df8-9a21-a604a160807f"), Name = "MUTTON 1" });
            modelBuilder.Entity<TypeOfAnimal>().HasData(new TypeOfAnimal { ID = Guid.Parse("b58b1bab-b7c0-424c-b1bd-6b28f7dc5173"), Name = "MUTTON 2" });
            modelBuilder.Entity<TypeOfAnimal>().HasData(new TypeOfAnimal { ID = Guid.Parse("c1147554-fd45-4381-9f66-eab06a34eea7"), Name = "MUTTON 5" });
            modelBuilder.Entity<TypeOfAnimal>().HasData(new TypeOfAnimal { ID = Guid.Parse("29106f56-4edb-4358-a4d5-cd234fe5212c"), Name = "MUTTON 6" });
            modelBuilder.Entity<TypeOfAnimal>().HasData(new TypeOfAnimal { ID = Guid.Parse("47272a57-5ce8-4663-9b55-3c881e399e0e"), Name = "LAMBS" });
            modelBuilder.Entity<TypeOfAnimal>().HasData(new TypeOfAnimal { ID = Guid.Parse("2c070ea8-4948-4cec-b6c4-dac09dd9b4de"), Name = "RAMS" });
            modelBuilder.Entity<TypeOfAnimal>().HasData(new TypeOfAnimal { ID = Guid.Parse("7a773a9b-025c-4400-b0d8-2eaecdd87068"), Name = "2 TOOTHS" });
            modelBuilder.Entity<TypeOfAnimal>().HasData(new TypeOfAnimal { ID = Guid.Parse("8c9f5370-d15e-4876-9c34-2223734d9ab5"), Name = "GOATS" });

            //modelBuilder.Entity<PriceXWeight>().HasData(new PriceXWeight { ID = Guid.Parse("0b9e3a1f-13e1-42e3-9078-86939283d352"), TypeOfAnimalID = Guid.Parse("2c070ea8-4948-4cec-b6c4-dac09dd9b4de"), WgtCl = 1, Min = 0.0m, Max = 9.9m, Price = 1m });
            //modelBuilder.Entity<PriceXWeight>().HasData(new PriceXWeight { ID = Guid.Parse("79d940c5-f4c1-451f-8b03-cc2b2a30bdf5"), TypeOfAnimalID = Guid.Parse("2c070ea8-4948-4cec-b6c4-dac09dd9b4de"), WgtCl = 2, Min = 10.0m, Max = 11.9m, Price = 1m });
            //modelBuilder.Entity<PriceXWeight>().HasData(new PriceXWeight { ID = Guid.Parse("9e8f4dd7-5915-4733-b68d-0da665d7cfbe"), TypeOfAnimalID = Guid.Parse("2c070ea8-4948-4cec-b6c4-dac09dd9b4de"), WgtCl = 3, Min = 12.0m, Max = 13.9m, Price = 1m });
            //modelBuilder.Entity<PriceXWeight>().HasData(new PriceXWeight { ID = Guid.Parse("74d95fcf-8c01-4d84-901d-f71f8d596149"), TypeOfAnimalID = Guid.Parse("2c070ea8-4948-4cec-b6c4-dac09dd9b4de"), WgtCl = 4, Min = 14.0m, Max = 15.9m, Price = 1m });
            //modelBuilder.Entity<PriceXWeight>().HasData(new PriceXWeight { ID = Guid.Parse("00890311-ecf5-4982-b0e8-ce11529bf50e"), TypeOfAnimalID = Guid.Parse("2c070ea8-4948-4cec-b6c4-dac09dd9b4de"), WgtCl = 5, Min = 16.0m, Max = 17.9m, Price = 1m });
            //modelBuilder.Entity<PriceXWeight>().HasData(new PriceXWeight { ID = Guid.Parse("c8eb740b-d2a8-4fd7-b82e-7d31ef0f24da"), TypeOfAnimalID = Guid.Parse("2c070ea8-4948-4cec-b6c4-dac09dd9b4de"), WgtCl = 6, Min = 18.0m, Max = 19.9m, Price = 2.5m});
            //modelBuilder.Entity<PriceXWeight>().HasData(new PriceXWeight { ID = Guid.Parse("653eedc0-ba41-4b09-8efb-14e8a0b02b0c"), TypeOfAnimalID = Guid.Parse("2c070ea8-4948-4cec-b6c4-dac09dd9b4de"), WgtCl = 7, Min = 20.0m, Max = 23.9m, Price = 2.5m });
            //modelBuilder.Entity<PriceXWeight>().HasData(new PriceXWeight { ID = Guid.Parse("aa99bd9f-2284-4a50-a7f5-d38676764211"), TypeOfAnimalID = Guid.Parse("2c070ea8-4948-4cec-b6c4-dac09dd9b4de"), WgtCl = 8, Min = 24.0m, Max = 25.9m, Price = 2.5m });
            //modelBuilder.Entity<PriceXWeight>().HasData(new PriceXWeight { ID = Guid.Parse("5f6802f3-c3d3-4350-bddd-24b86e338e17"), TypeOfAnimalID = Guid.Parse("2c070ea8-4948-4cec-b6c4-dac09dd9b4de"), WgtCl = 9, Min = 26.0m, Max = 29.9m, Price = 2.5m });
            //modelBuilder.Entity<PriceXWeight>().HasData(new PriceXWeight { ID = Guid.Parse("4994f759-0d30-474c-a992-113ca6ee1de0"), TypeOfAnimalID = Guid.Parse("2c070ea8-4948-4cec-b6c4-dac09dd9b4de"), WgtCl = 10, Min = 30.0m, Max = 31.9m, Price =  2m});
            //modelBuilder.Entity<PriceXWeight>().HasData(new PriceXWeight { ID = Guid.Parse("35298469-d786-453d-808a-5ca4e90dcbb8"), TypeOfAnimalID = Guid.Parse("2c070ea8-4948-4cec-b6c4-dac09dd9b4de"), WgtCl = 11, Min = 32.0m, Max = 34.9m, Price = 2m });
            //modelBuilder.Entity<PriceXWeight>().HasData(new PriceXWeight { ID = Guid.Parse("42efb241-9da0-4987-953e-ed022daddf00"), TypeOfAnimalID = Guid.Parse("2c070ea8-4948-4cec-b6c4-dac09dd9b4de"), WgtCl = 12, Min = 35.0m, Max = 80.0m, Price = 2m });


            modelBuilder.Entity<Buyer>().HasData(new Buyer { ID  = Guid.Parse( "24aafc27-b78a-436b-991f-676fa6998f41"),  Code = "AT" });
            modelBuilder.Entity<Buyer>().HasData(new Buyer { ID = Guid.Parse("c2caed27-70f8-41ae-80b1-fa6f2e12210d"), Code = "GA" });
            modelBuilder.Entity<Buyer>().HasData(new Buyer { ID = Guid.Parse("6abab1df-f969-43a7-8666-69c2ea6abb17"), Code = "GC" });
            modelBuilder.Entity<Buyer>().HasData(new Buyer { ID = Guid.Parse("42302447-7e0f-41be-9dca-a5507ec97fb3"), Code = "GS" });
            modelBuilder.Entity<Buyer>().HasData(new Buyer { ID = Guid.Parse("aa27d3af-c310-4715-8cb3-8901c8af7611"), Code = "MS" });

            

            modelBuilder.Entity<Models.Agent>().HasData(new Models.Agent { ID = Guid.Parse("f197a2b8-2c2c-4793-9f5f-e9c66188029d"), Name = "ROBERTS",Hide=false });
            modelBuilder.Entity<Models.Agent>().HasData(new Models.Agent { ID = Guid.Parse("6b34fbce-2b44-46fb-9d7a-e5afa2428afc"), Name = "DEGARIS", Hide = false });
            modelBuilder.Entity<Models.Agent>().HasData(new Models.Agent { ID = Guid.Parse("7661bae2-d8be-4b9b-8c18-f4b82bb7e788"), Name = "DIRECT", Hide = false });
            modelBuilder.Entity<Models.Agent>().HasData(new Models.Agent { ID = Guid.Parse("1b999108-2ddb-4bbd-8d2b-f35a2ebd8c30"), Name = "MKT", Hide = false });

            modelBuilder.Entity<Models.Location>().HasData(new Models.Location { ID = Guid.Parse("0afd5f6c-128d-4a76-8c0f-5959d142f2cd"), Name = "TASMANIA", Hide = false });
            modelBuilder.Entity<Models.Location>().HasData(new Models.Location { ID = Guid.Parse("34e0c846-7774-4788-bbda-6d06590078cc"), Name = "PENOLA", Hide = false });
            modelBuilder.Entity<Models.Location>().HasData(new Models.Location { ID = Guid.Parse("d302f55d-7fe9-48c4-a8ca-5add00ad9264"), Name = "ARARAT", Hide = false });
            modelBuilder.Entity<Models.Location>().HasData(new Models.Location { ID = Guid.Parse("9121a4bc-494f-4fc3-96a2-07541b0b5707"), Name = "BENDIGO", Hide = false });

            //modelBuilder.Entity<Models.Vendor>().HasData(new Models.Vendor { ID = Guid.Parse("53f0c984-d498-464e-8d88-ed9baa94fd76"), Name = "TASWOOL" });
            //modelBuilder.Entity<Models.Vendor>().HasData(new Models.Vendor { ID = Guid.Parse("1caddddb-6cc5-4670-b23c-3965f5d8ec38"), Name = "KONLEIGH" });
            //modelBuilder.Entity<Models.Vendor>().HasData(new Models.Vendor { ID = Guid.Parse("692ebf1e-420d-4857-a6c6-af6cc84cb7fd"), Name = "McKENZIE" });
            //modelBuilder.Entity<Models.Vendor>().HasData(new Models.Vendor { ID = Guid.Parse("b486e825-2c7c-4bf1-8033-c677a0cc463c"), Name = "FOREST LODGE" });

            modelBuilder.Entity<Models.Transport>().HasData(new Models.Transport { ID = Guid.Parse("c0f792a2-b10a-43d8-a760-222752bf15a7"), Name = "O'SULLIVANS" });
            modelBuilder.Entity<Models.Transport>().HasData(new Models.Transport { ID = Guid.Parse("5938b059-ad03-48f7-95ba-ca9c6c7e5f2e"), Name = "O'T" });
            modelBuilder.Entity<Models.Transport>().HasData(new Models.Transport { ID = Guid.Parse("c3741149-1f3a-4021-8fae-7e1a6e323667"), Name = "PAGE" });
            modelBuilder.Entity<Models.Transport>().HasData(new Models.Transport { ID = Guid.Parse("d5280ba4-f927-4a4e-a7d1-7254f6758140"), Name = "STEVENS" });

            modelBuilder.Entity<Models.UserOpenInvitation>().HasData(new Models.UserOpenInvitation {  Id = Guid.Parse("d5280ba4-f927-4a4e-a7d1-7254f6758140"),  Email = "feliceferri@gmail.com" });

            modelBuilder.Entity<BuyType>().HasData(new BuyType { ID = Guid.Parse("d180ff28-1321-467a-a0de-d7955d463762"), Name = "Private Buy" }); //OLD Price Per Head Buy
            modelBuilder.Entity<BuyType>().HasData(new BuyType { ID = Guid.Parse("29dec9c6-c2c3-4603-9c37-3256ab99215a"), Name = "Market Buy" });
            modelBuilder.Entity<BuyType>().HasData(new BuyType { ID = Guid.Parse("196d17bd-5320-47da-9d44-57ff705c18b9"), Name = "Hooks Buy" });
        }

        public DbSet<livestock.Models.WeightSheet> WeightSheet { get; set; }
    }
}

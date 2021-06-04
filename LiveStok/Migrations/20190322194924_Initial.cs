using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LiveStok.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agent",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agent", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buyer",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BuyTypes",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Locationts",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locationts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Transport",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transport", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfAnimals",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfAnimals", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserOpenInvitations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOpenInvitations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockPurchase",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    BuyerNo = table.Column<string>(nullable: true),
                    BuyerID = table.Column<Guid>(nullable: false),
                    TypeOfAnimalID = table.Column<Guid>(nullable: false),
                    BuyTypeID = table.Column<Guid>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Freight = table.Column<decimal>(nullable: false),
                    EstimatedWeight = table.Column<decimal>(nullable: false),
                    AgentID = table.Column<Guid>(nullable: false),
                    LocationID = table.Column<Guid>(nullable: false),
                    VendorID = table.Column<Guid>(nullable: false),
                    IntendedDeliveryDate = table.Column<DateTime>(nullable: false),
                    TransportID = table.Column<Guid>(nullable: false),
                    DateDelivered = table.Column<DateTime>(nullable: true),
                    NumberDelivered = table.Column<int>(nullable: false),
                    YTBDelivered = table.Column<int>(nullable: false),
                    InvoiceRecD = table.Column<bool>(nullable: false),
                    ContactName = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: true),
                    ContactFax = table.Column<string>(nullable: true),
                    PurchaseType = table.Column<string>(nullable: true),
                    WCl1Price = table.Column<decimal>(nullable: true),
                    WCl2Price = table.Column<decimal>(nullable: true),
                    WCl3Price = table.Column<decimal>(nullable: true),
                    WCl4Price = table.Column<decimal>(nullable: true),
                    WCl5Price = table.Column<decimal>(nullable: true),
                    WCl6Price = table.Column<decimal>(nullable: true),
                    WCl7Price = table.Column<decimal>(nullable: true),
                    WCl8Price = table.Column<decimal>(nullable: true),
                    WCl9Price = table.Column<decimal>(nullable: true),
                    WCl10Price = table.Column<decimal>(nullable: true),
                    WCl11Price = table.Column<decimal>(nullable: true),
                    WCl12Price = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockPurchase", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StockPurchase_Agent",
                        column: x => x.AgentID,
                        principalTable: "Agent",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockPurchase_BuyTypes_BuyTypeID",
                        column: x => x.BuyTypeID,
                        principalTable: "BuyTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockPurchase_Buyer",
                        column: x => x.BuyerID,
                        principalTable: "Buyer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockPurchase_Location",
                        column: x => x.LocationID,
                        principalTable: "Locationts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockPurchase_Transport",
                        column: x => x.TransportID,
                        principalTable: "Transport",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockPurchase_TypeOfAnimal",
                        column: x => x.TypeOfAnimalID,
                        principalTable: "TypeOfAnimals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockPurchase_Vendor",
                        column: x => x.VendorID,
                        principalTable: "Vendor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarketBuys",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    StockPurchaseID = table.Column<Guid>(nullable: false),
                    AgentID = table.Column<Guid>(nullable: true),
                    Number = table.Column<int>(nullable: true),
                    Price = table.Column<decimal>(nullable: true),
                    CodeDesc = table.Column<string>(nullable: true),
                    TypeOfAnimalID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketBuys", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MarketBuys_StockPurchase_StockPurchaseID",
                        column: x => x.StockPurchaseID,
                        principalTable: "StockPurchase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MarketBuys_TypeOfAnimals_TypeOfAnimalID",
                        column: x => x.TypeOfAnimalID,
                        principalTable: "TypeOfAnimals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MarketBuySummaries",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    StockPurchaseID = table.Column<Guid>(nullable: false),
                    TypeOfAnimalID = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    AvPrice = table.Column<decimal>(nullable: true),
                    Freight = table.Column<decimal>(nullable: true),
                    Skin = table.Column<decimal>(nullable: true),
                    AvWeight = table.Column<decimal>(nullable: true),
                    CostXKg = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketBuySummaries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MarketBuySummary_StockPurchase",
                        column: x => x.StockPurchaseID,
                        principalTable: "StockPurchase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PricePerHeadBuy",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    StockPurchaseID = table.Column<Guid>(nullable: false),
                    HeadsBought = table.Column<int>(nullable: true),
                    Price = table.Column<decimal>(nullable: true),
                    Skin = table.Column<decimal>(nullable: true),
                    Freight = table.Column<decimal>(nullable: true),
                    Weight = table.Column<decimal>(nullable: true),
                    TypeOfAnimalID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricePerHeadBuy", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PricePerHeadBuy_StockPurchase",
                        column: x => x.StockPurchaseID,
                        principalTable: "StockPurchase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PricePerHeadBuy_TypeOfAnimal",
                        column: x => x.TypeOfAnimalID,
                        principalTable: "TypeOfAnimals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WeightSheet",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    StockPurchaseID = table.Column<Guid>(nullable: false),
                    LotNumber = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Origin = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Skin = table.Column<decimal>(nullable: false),
                    WeightOff = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightSheet", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StockPurchase_WeightSheet",
                        column: x => x.StockPurchaseID,
                        principalTable: "StockPurchase",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weights",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    WeightSheetID = table.Column<Guid>(nullable: false),
                    weight = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weights", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Weight_WeightSheet",
                        column: x => x.WeightSheetID,
                        principalTable: "WeightSheet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Agent",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { new Guid("f197a2b8-2c2c-4793-9f5f-e9c66188029d"), "ROBERTS" },
                    { new Guid("6b34fbce-2b44-46fb-9d7a-e5afa2428afc"), "DEGARIS" },
                    { new Guid("7661bae2-d8be-4b9b-8c18-f4b82bb7e788"), "DIRECT" },
                    { new Guid("1b999108-2ddb-4bbd-8d2b-f35a2ebd8c30"), "MKT" }
                });

            migrationBuilder.InsertData(
                table: "BuyTypes",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { new Guid("d180ff28-1321-467a-a0de-d7955d463762"), "Private Buy" },
                    { new Guid("29dec9c6-c2c3-4603-9c37-3256ab99215a"), "Market Buy" },
                    { new Guid("196d17bd-5320-47da-9d44-57ff705c18b9"), "Hooks Buy" }
                });

            migrationBuilder.InsertData(
                table: "Buyer",
                columns: new[] { "ID", "Code" },
                values: new object[,]
                {
                    { new Guid("24aafc27-b78a-436b-991f-676fa6998f41"), "AT" },
                    { new Guid("c2caed27-70f8-41ae-80b1-fa6f2e12210d"), "GA" },
                    { new Guid("6abab1df-f969-43a7-8666-69c2ea6abb17"), "GC" },
                    { new Guid("42302447-7e0f-41be-9dca-a5507ec97fb3"), "GS" },
                    { new Guid("aa27d3af-c310-4715-8cb3-8901c8af7611"), "MS" }
                });

            migrationBuilder.InsertData(
                table: "Locationts",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { new Guid("9121a4bc-494f-4fc3-96a2-07541b0b5707"), "BENDIGO" },
                    { new Guid("d302f55d-7fe9-48c4-a8ca-5add00ad9264"), "ARARAT" },
                    { new Guid("0afd5f6c-128d-4a76-8c0f-5959d142f2cd"), "TASMANIA" },
                    { new Guid("34e0c846-7774-4788-bbda-6d06590078cc"), "PENOLA" }
                });

            migrationBuilder.InsertData(
                table: "Transport",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { new Guid("c0f792a2-b10a-43d8-a760-222752bf15a7"), "O'SULLIVANS" },
                    { new Guid("5938b059-ad03-48f7-95ba-ca9c6c7e5f2e"), "O'T" },
                    { new Guid("c3741149-1f3a-4021-8fae-7e1a6e323667"), "PAGE" },
                    { new Guid("d5280ba4-f927-4a4e-a7d1-7254f6758140"), "STEVENS" }
                });

            migrationBuilder.InsertData(
                table: "TypeOfAnimals",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { new Guid("1550b1b3-43aa-4df8-9a21-a604a160807f"), "MUTTON" },
                    { new Guid("47272a57-5ce8-4663-9b55-3c881e399e0e"), "LAMBS" },
                    { new Guid("2c070ea8-4948-4cec-b6c4-dac09dd9b4de"), "RAMS" },
                    { new Guid("7a773a9b-025c-4400-b0d8-2eaecdd87068"), "2 TOOTHS" }
                });

            migrationBuilder.InsertData(
                table: "UserOpenInvitations",
                columns: new[] { "Id", "Email" },
                values: new object[] { new Guid("d5280ba4-f927-4a4e-a7d1-7254f6758140"), "feliceferri@gmail.com" });

            migrationBuilder.InsertData(
                table: "Vendor",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { new Guid("692ebf1e-420d-4857-a6c6-af6cc84cb7fd"), "McKENZIE" },
                    { new Guid("53f0c984-d498-464e-8d88-ed9baa94fd76"), "TASWOOL" },
                    { new Guid("1caddddb-6cc5-4670-b23c-3965f5d8ec38"), "KONLEIGH" },
                    { new Guid("b486e825-2c7c-4bf1-8033-c677a0cc463c"), "FOREST LODGE" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MarketBuys_StockPurchaseID",
                table: "MarketBuys",
                column: "StockPurchaseID");

            migrationBuilder.CreateIndex(
                name: "IX_MarketBuys_TypeOfAnimalID",
                table: "MarketBuys",
                column: "TypeOfAnimalID");

            migrationBuilder.CreateIndex(
                name: "IX_MarketBuySummaries_StockPurchaseID",
                table: "MarketBuySummaries",
                column: "StockPurchaseID");

            migrationBuilder.CreateIndex(
                name: "IX_PricePerHeadBuy_StockPurchaseID",
                table: "PricePerHeadBuy",
                column: "StockPurchaseID");

            migrationBuilder.CreateIndex(
                name: "IX_PricePerHeadBuy_TypeOfAnimalID",
                table: "PricePerHeadBuy",
                column: "TypeOfAnimalID");

            migrationBuilder.CreateIndex(
                name: "IX_StockPurchase_AgentID",
                table: "StockPurchase",
                column: "AgentID");

            migrationBuilder.CreateIndex(
                name: "IX_StockPurchase_BuyTypeID",
                table: "StockPurchase",
                column: "BuyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_StockPurchase_BuyerID",
                table: "StockPurchase",
                column: "BuyerID");

            migrationBuilder.CreateIndex(
                name: "IX_StockPurchase_LocationID",
                table: "StockPurchase",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_StockPurchase_TransportID",
                table: "StockPurchase",
                column: "TransportID");

            migrationBuilder.CreateIndex(
                name: "IX_StockPurchase_TypeOfAnimalID",
                table: "StockPurchase",
                column: "TypeOfAnimalID");

            migrationBuilder.CreateIndex(
                name: "IX_StockPurchase_VendorID",
                table: "StockPurchase",
                column: "VendorID");

            migrationBuilder.CreateIndex(
                name: "IX_Weights_WeightSheetID",
                table: "Weights",
                column: "WeightSheetID");

            migrationBuilder.CreateIndex(
                name: "IX_WeightSheet_StockPurchaseID",
                table: "WeightSheet",
                column: "StockPurchaseID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "MarketBuys");

            migrationBuilder.DropTable(
                name: "MarketBuySummaries");

            migrationBuilder.DropTable(
                name: "PricePerHeadBuy");

            migrationBuilder.DropTable(
                name: "UserOpenInvitations");

            migrationBuilder.DropTable(
                name: "Weights");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "WeightSheet");

            migrationBuilder.DropTable(
                name: "StockPurchase");

            migrationBuilder.DropTable(
                name: "Agent");

            migrationBuilder.DropTable(
                name: "BuyTypes");

            migrationBuilder.DropTable(
                name: "Buyer");

            migrationBuilder.DropTable(
                name: "Locationts");

            migrationBuilder.DropTable(
                name: "Transport");

            migrationBuilder.DropTable(
                name: "TypeOfAnimals");

            migrationBuilder.DropTable(
                name: "Vendor");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlobalTradeSevkiyatTakip.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class MGRT_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cari",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WolvoxBlKodu = table.Column<long>(type: "bigint", nullable: true),
                    CariKodu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicariUnvan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TcNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VergiDairesi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VergiNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iletisim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlusturanKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncelleyenKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncellemeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cari", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Depo",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WolvoxBlKodu = table.Column<long>(type: "bigint", nullable: true),
                    DepoAd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepoAdres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepoIletisim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepoYetkili = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlusturanKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncelleyenKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncellemeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depo", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Doviz",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WolvoxBlKodu = table.Column<long>(type: "bigint", nullable: true),
                    DovizBirim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlisFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SatisFiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OzelKod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlusturanKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncelleyenKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncellemeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doviz", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EntegreKullanici",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parola = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeniHatirla = table.Column<bool>(type: "bit", nullable: false),
                    Rol = table.Column<int>(type: "int", nullable: false),
                    OlusturanKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncelleyenKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncellemeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntegreKullanici", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Hizmet",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WolvoxBlKodu = table.Column<long>(type: "bigint", nullable: true),
                    HizmetAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlusturanKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncelleyenKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncellemeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hizmet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Kullanici",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Parola = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rol = table.Column<int>(type: "int", nullable: true),
                    OlusturanKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncelleyenKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncellemeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanici", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Marka",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarkaAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlusturanKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncelleyenKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncellemeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marka", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ParaBirim",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirimSimge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlusturanKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncelleyenKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncellemeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParaBirim", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Renk",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RenkAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlusturanKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncelleyenKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncellemeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renk", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sevkiyat",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SevkiyatTarih = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SevkiyatAdres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AracPlaka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoforAdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NetMaliyet = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Kar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Zarar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OlusturanKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncelleyenKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncellemeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sevkiyat", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Fatura",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WolvoxBlKodu = table.Column<long>(type: "bigint", nullable: true),
                    CariId = table.Column<int>(type: "int", nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FaturaNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DovizId = table.Column<int>(type: "int", nullable: true),
                    ProjeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WolvoxFaturaTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WolvoxDolarTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    WolvoxDovizBirimTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FaturaTur = table.Column<int>(type: "int", nullable: true),
                    EvrakTur = table.Column<int>(type: "int", nullable: true),
                    OlusturanKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncelleyenKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncellemeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fatura", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Fatura_Cari_CariId",
                        column: x => x.CariId,
                        principalTable: "Cari",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Fatura_Doviz_DovizId",
                        column: x => x.DovizId,
                        principalTable: "Doviz",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Irsaliye",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WolvoxBlKodu = table.Column<long>(type: "bigint", nullable: true),
                    ProjeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IrsaliyeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IrsaliyeTarih = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SevkTarih = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SevkAdres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CariId = table.Column<int>(type: "int", nullable: true),
                    DovizId = table.Column<int>(type: "int", nullable: true),
                    SevkDurum = table.Column<int>(type: "int", nullable: true),
                    IrsaliyeTur = table.Column<int>(type: "int", nullable: true),
                    FaturaDurum = table.Column<int>(type: "int", nullable: true),
                    WolvoxGonderimDurum = table.Column<int>(type: "int", nullable: true),
                    EvrakTur = table.Column<int>(type: "int", nullable: true),
                    OlusturanKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncelleyenKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncellemeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Irsaliye", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Irsaliye_Cari_CariId",
                        column: x => x.CariId,
                        principalTable: "Cari",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Irsaliye_Doviz_DovizId",
                        column: x => x.DovizId,
                        principalTable: "Doviz",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SevkiyatNot",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Metin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KullaniciId = table.Column<int>(type: "int", nullable: true),
                    OlusturanKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncelleyenKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncellemeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SevkiyatNot", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SevkiyatNot_Kullanici_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanici",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Stok",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WolvoxBlKodu = table.Column<long>(type: "bigint", nullable: true),
                    StokKod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barkod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StokAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KdvOran = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RenkId = table.Column<int>(type: "int", nullable: true),
                    MarkaId = table.Column<int>(type: "int", nullable: true),
                    DepoId = table.Column<int>(type: "int", nullable: true),
                    IskontoOran = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Agirlik = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DesiMiktari = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DovizBirim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlusturanKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncelleyenKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncellemeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stok", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Stok_Depo_DepoId",
                        column: x => x.DepoId,
                        principalTable: "Depo",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Stok_Marka_MarkaId",
                        column: x => x.MarkaId,
                        principalTable: "Marka",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Stok_Renk_RenkId",
                        column: x => x.RenkId,
                        principalTable: "Renk",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SevkiyatDetay",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SevkiyatId = table.Column<int>(type: "int", nullable: true),
                    IrsaliyeId = table.Column<int>(type: "int", nullable: true),
                    FaturaId = table.Column<int>(type: "int", nullable: true),
                    OlusturanKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncelleyenKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncellemeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SevkiyatDetay", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SevkiyatDetay_Fatura_FaturaId",
                        column: x => x.FaturaId,
                        principalTable: "Fatura",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SevkiyatDetay_Irsaliye_IrsaliyeId",
                        column: x => x.IrsaliyeId,
                        principalTable: "Irsaliye",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SevkiyatDetay_Sevkiyat_SevkiyatId",
                        column: x => x.SevkiyatId,
                        principalTable: "Sevkiyat",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "DepoDetay",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WolvoxBlKodu = table.Column<long>(type: "bigint", nullable: true),
                    DepoId = table.Column<int>(type: "int", nullable: true),
                    StokId = table.Column<int>(type: "int", nullable: true),
                    CariId = table.Column<int>(type: "int", nullable: true),
                    Adet = table.Column<int>(type: "int", nullable: true),
                    TarihSaat = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OlusturanKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncelleyenKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncellemeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepoDetay", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DepoDetay_Cari_CariId",
                        column: x => x.CariId,
                        principalTable: "Cari",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_DepoDetay_Depo_DepoId",
                        column: x => x.DepoId,
                        principalTable: "Depo",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_DepoDetay_Stok_StokId",
                        column: x => x.StokId,
                        principalTable: "Stok",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FaturaDetay",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WolvoxBlKodu = table.Column<long>(type: "bigint", nullable: true),
                    FaturaId = table.Column<int>(type: "int", nullable: true),
                    StokId = table.Column<int>(type: "int", nullable: true),
                    DovizId = table.Column<int>(type: "int", nullable: true),
                    HizmetId = table.Column<int>(type: "int", nullable: true),
                    Miktar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ToplamTutar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OlusturanKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncelleyenKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncellemeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaturaDetay", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FaturaDetay_Doviz_DovizId",
                        column: x => x.DovizId,
                        principalTable: "Doviz",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FaturaDetay_Fatura_FaturaId",
                        column: x => x.FaturaId,
                        principalTable: "Fatura",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FaturaDetay_Hizmet_HizmetId",
                        column: x => x.HizmetId,
                        principalTable: "Hizmet",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_FaturaDetay_Stok_StokId",
                        column: x => x.StokId,
                        principalTable: "Stok",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "IrsaliyeDetay",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WolvoxBlKodu = table.Column<long>(type: "bigint", nullable: true),
                    IrsaliyeId = table.Column<int>(type: "int", nullable: true),
                    StokId = table.Column<int>(type: "int", nullable: true),
                    HizmetId = table.Column<int>(type: "int", nullable: true),
                    DepoId = table.Column<int>(type: "int", nullable: true),
                    Miktar = table.Column<int>(type: "int", nullable: true),
                    TakimDurum = table.Column<bool>(type: "bit", nullable: true),
                    Icindekiler = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Olculer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KapNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaketSekil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaketIciAdet = table.Column<int>(type: "int", nullable: true),
                    ToplamPaketIciAdet = table.Column<int>(type: "int", nullable: true),
                    KapAdet = table.Column<int>(type: "int", nullable: true),
                    ToplamKapAdet = table.Column<int>(type: "int", nullable: true),
                    TedarikFirma = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrunIcerik = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrunBurutAgirlik = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UrunNetAgirlik = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OlusturanKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncelleyenKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncellemeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IrsaliyeDetay", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IrsaliyeDetay_Depo_DepoId",
                        column: x => x.DepoId,
                        principalTable: "Depo",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_IrsaliyeDetay_Hizmet_HizmetId",
                        column: x => x.HizmetId,
                        principalTable: "Hizmet",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_IrsaliyeDetay_Irsaliye_IrsaliyeId",
                        column: x => x.IrsaliyeId,
                        principalTable: "Irsaliye",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_IrsaliyeDetay_Stok_StokId",
                        column: x => x.StokId,
                        principalTable: "Stok",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "StokDetay",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WolvoxBlKodu = table.Column<long>(type: "bigint", nullable: true),
                    DepoId = table.Column<int>(type: "int", nullable: true),
                    CariId = table.Column<int>(type: "int", nullable: true),
                    StokId = table.Column<int>(type: "int", nullable: true),
                    GirisMiktar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CikisMiktar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetMiktar = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OlusturanKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncelleyenKullanici = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuncellemeTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OlusturmaTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StokDetay", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StokDetay_Cari_CariId",
                        column: x => x.CariId,
                        principalTable: "Cari",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StokDetay_Depo_DepoId",
                        column: x => x.DepoId,
                        principalTable: "Depo",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_StokDetay_Stok_StokId",
                        column: x => x.StokId,
                        principalTable: "Stok",
                        principalColumn: "ID");
                });

            migrationBuilder.InsertData(
                table: "EntegreKullanici",
                columns: new[] { "ID", "BeniHatirla", "Email", "GuncellemeTarih", "GuncelleyenKullanici", "IsDeleted", "OlusturanKullanici", "OlusturmaTarih", "Parola", "Rol" },
                values: new object[] { 1, false, "entegre@gmail.com", new DateTime(2023, 9, 8, 16, 13, 54, 99, DateTimeKind.Local).AddTicks(2935), "asd", false, "sd", new DateTime(2023, 9, 8, 16, 13, 54, 99, DateTimeKind.Local).AddTicks(2935), "jyBAdWogYwoYrNvTdwxwLKavHQiYqszX7dUAggFIfZhdDQFBvLfU1q3UMnCe1yVo", 3 });

            migrationBuilder.InsertData(
                table: "Kullanici",
                columns: new[] { "ID", "Ad", "Email", "GuncellemeTarih", "GuncelleyenKullanici", "IsDeleted", "OlusturanKullanici", "OlusturmaTarih", "Parola", "Rol", "Soyad" },
                values: new object[] { 1, "default", "default@gmail.com", new DateTime(2023, 9, 8, 16, 13, 54, 99, DateTimeKind.Local).AddTicks(2446), "asd", false, "sd", new DateTime(2023, 9, 8, 16, 13, 54, 99, DateTimeKind.Local).AddTicks(2474), "jyBAdWogYwoYrNvTdwxwLKavHQiYqszX7dUAggFIfZhdDQFBvLfU1q3UMnCe1yVo", 2, "default" });

            migrationBuilder.CreateIndex(
                name: "IX_DepoDetay_CariId",
                table: "DepoDetay",
                column: "CariId");

            migrationBuilder.CreateIndex(
                name: "IX_DepoDetay_DepoId",
                table: "DepoDetay",
                column: "DepoId");

            migrationBuilder.CreateIndex(
                name: "IX_DepoDetay_StokId",
                table: "DepoDetay",
                column: "StokId");

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_CariId",
                table: "Fatura",
                column: "CariId");

            migrationBuilder.CreateIndex(
                name: "IX_Fatura_DovizId",
                table: "Fatura",
                column: "DovizId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturaDetay_DovizId",
                table: "FaturaDetay",
                column: "DovizId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturaDetay_FaturaId",
                table: "FaturaDetay",
                column: "FaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturaDetay_HizmetId",
                table: "FaturaDetay",
                column: "HizmetId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturaDetay_StokId",
                table: "FaturaDetay",
                column: "StokId");

            migrationBuilder.CreateIndex(
                name: "IX_Irsaliye_CariId",
                table: "Irsaliye",
                column: "CariId");

            migrationBuilder.CreateIndex(
                name: "IX_Irsaliye_DovizId",
                table: "Irsaliye",
                column: "DovizId");

            migrationBuilder.CreateIndex(
                name: "IX_IrsaliyeDetay_DepoId",
                table: "IrsaliyeDetay",
                column: "DepoId");

            migrationBuilder.CreateIndex(
                name: "IX_IrsaliyeDetay_HizmetId",
                table: "IrsaliyeDetay",
                column: "HizmetId");

            migrationBuilder.CreateIndex(
                name: "IX_IrsaliyeDetay_IrsaliyeId",
                table: "IrsaliyeDetay",
                column: "IrsaliyeId");

            migrationBuilder.CreateIndex(
                name: "IX_IrsaliyeDetay_StokId",
                table: "IrsaliyeDetay",
                column: "StokId");

            migrationBuilder.CreateIndex(
                name: "IX_SevkiyatDetay_FaturaId",
                table: "SevkiyatDetay",
                column: "FaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_SevkiyatDetay_IrsaliyeId",
                table: "SevkiyatDetay",
                column: "IrsaliyeId");

            migrationBuilder.CreateIndex(
                name: "IX_SevkiyatDetay_SevkiyatId",
                table: "SevkiyatDetay",
                column: "SevkiyatId");

            migrationBuilder.CreateIndex(
                name: "IX_SevkiyatNot_KullaniciId",
                table: "SevkiyatNot",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_Stok_DepoId",
                table: "Stok",
                column: "DepoId");

            migrationBuilder.CreateIndex(
                name: "IX_Stok_MarkaId",
                table: "Stok",
                column: "MarkaId");

            migrationBuilder.CreateIndex(
                name: "IX_Stok_RenkId",
                table: "Stok",
                column: "RenkId");

            migrationBuilder.CreateIndex(
                name: "IX_StokDetay_CariId",
                table: "StokDetay",
                column: "CariId");

            migrationBuilder.CreateIndex(
                name: "IX_StokDetay_DepoId",
                table: "StokDetay",
                column: "DepoId");

            migrationBuilder.CreateIndex(
                name: "IX_StokDetay_StokId",
                table: "StokDetay",
                column: "StokId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepoDetay");

            migrationBuilder.DropTable(
                name: "EntegreKullanici");

            migrationBuilder.DropTable(
                name: "FaturaDetay");

            migrationBuilder.DropTable(
                name: "IrsaliyeDetay");

            migrationBuilder.DropTable(
                name: "ParaBirim");

            migrationBuilder.DropTable(
                name: "SevkiyatDetay");

            migrationBuilder.DropTable(
                name: "SevkiyatNot");

            migrationBuilder.DropTable(
                name: "StokDetay");

            migrationBuilder.DropTable(
                name: "Hizmet");

            migrationBuilder.DropTable(
                name: "Fatura");

            migrationBuilder.DropTable(
                name: "Irsaliye");

            migrationBuilder.DropTable(
                name: "Sevkiyat");

            migrationBuilder.DropTable(
                name: "Kullanici");

            migrationBuilder.DropTable(
                name: "Stok");

            migrationBuilder.DropTable(
                name: "Cari");

            migrationBuilder.DropTable(
                name: "Doviz");

            migrationBuilder.DropTable(
                name: "Depo");

            migrationBuilder.DropTable(
                name: "Marka");

            migrationBuilder.DropTable(
                name: "Renk");
        }
    }
}

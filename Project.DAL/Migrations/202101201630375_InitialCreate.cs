namespace Project.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kategoriler",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Kategori = c.String(nullable: false, maxLength: 50),
                        Aciklama = c.String(maxLength: 150),
                        ModifiedUsername = c.String(),
                        VeriYaratmaTarihi = c.DateTime(name: "Veri Yaratma Tarihi", precision: 7, storeType: "datetime2"),
                        VeriGüncellemeTarihi = c.DateTime(name: "Veri Güncelleme Tarihi", precision: 7, storeType: "datetime2"),
                        VeriSilmeTarihi = c.DateTime(name: "Veri Silme Tarihi", precision: 7, storeType: "datetime2"),
                        VeriDurumu = c.Int(name: "Veri Durumu"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Notlar",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NotBasligi = c.String(name: "Not Basligi", nullable: false, maxLength: 60),
                        NotMetni = c.String(name: "Not Metni", nullable: false, maxLength: 2000),
                        Taslak = c.Boolean(nullable: false),
                        Begenilme = c.Int(nullable: false),
                        Kategori = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        ModifiedUsername = c.String(),
                        VeriYaratmaTarihi = c.DateTime(name: "Veri Yaratma Tarihi", precision: 7, storeType: "datetime2"),
                        VeriGüncellemeTarihi = c.DateTime(name: "Veri Güncelleme Tarihi", precision: 7, storeType: "datetime2"),
                        VeriSilmeTarihi = c.DateTime(name: "Veri Silme Tarihi", precision: 7, storeType: "datetime2"),
                        VeriDurumu = c.Int(name: "Veri Durumu"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Kullanicilar", t => t.UserID)
                .ForeignKey("dbo.Kategoriler", t => t.Kategori)
                .Index(t => t.Kategori)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Yorumlar",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 300),
                        ModifiedUsername = c.String(),
                        VeriYaratmaTarihi = c.DateTime(name: "Veri Yaratma Tarihi", precision: 7, storeType: "datetime2"),
                        VeriGüncellemeTarihi = c.DateTime(name: "Veri Güncelleme Tarihi", precision: 7, storeType: "datetime2"),
                        VeriSilmeTarihi = c.DateTime(name: "Veri Silme Tarihi", precision: 7, storeType: "datetime2"),
                        VeriDurumu = c.Int(name: "Veri Durumu"),
                        Note_ID = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Notlar", t => t.Note_ID)
                .ForeignKey("dbo.Kullanicilar", t => t.User_ID)
                .Index(t => t.Note_ID)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Kullanicilar",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        isim = c.String(maxLength: 25),
                        Soyad = c.String(maxLength: 25),
                        KullaniciAdi = c.String(name: "Kullanici Adi", nullable: false, maxLength: 25),
                        EPosta = c.String(name: "E-Posta", nullable: false, maxLength: 70),
                        Sifre = c.String(nullable: false, maxLength: 25),
                        ProfileImageFileName = c.String(maxLength: 30),
                        Aktif = c.Boolean(),
                        Yonetici = c.Boolean(),
                        ActivateGuid = c.Guid(),
                        ModifiedUsername = c.String(),
                        VeriYaratmaTarihi = c.DateTime(name: "Veri Yaratma Tarihi", precision: 7, storeType: "datetime2"),
                        VeriGüncellemeTarihi = c.DateTime(name: "Veri Güncelleme Tarihi", precision: 7, storeType: "datetime2"),
                        VeriSilmeTarihi = c.DateTime(name: "Veri Silme Tarihi", precision: 7, storeType: "datetime2"),
                        VeriDurumu = c.Int(name: "Veri Durumu"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Likeds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NoteID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        ModifiedUsername = c.String(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        DeletedDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Kullanicilar", t => t.UserID, cascadeDelete: true)
                .ForeignKey("dbo.Notlar", t => t.NoteID, cascadeDelete: true)
                .Index(t => t.NoteID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notlar", "Kategori", "dbo.Kategoriler");
            DropForeignKey("dbo.Notlar", "UserID", "dbo.Kullanicilar");
            DropForeignKey("dbo.Likeds", "NoteID", "dbo.Notlar");
            DropForeignKey("dbo.Likeds", "UserID", "dbo.Kullanicilar");
            DropForeignKey("dbo.Yorumlar", "User_ID", "dbo.Kullanicilar");
            DropForeignKey("dbo.Yorumlar", "Note_ID", "dbo.Notlar");
            DropIndex("dbo.Likeds", new[] { "UserID" });
            DropIndex("dbo.Likeds", new[] { "NoteID" });
            DropIndex("dbo.Yorumlar", new[] { "User_ID" });
            DropIndex("dbo.Yorumlar", new[] { "Note_ID" });
            DropIndex("dbo.Notlar", new[] { "UserID" });
            DropIndex("dbo.Notlar", new[] { "Kategori" });
            DropTable("dbo.Likeds");
            DropTable("dbo.Kullanicilar");
            DropTable("dbo.Yorumlar");
            DropTable("dbo.Notlar");
            DropTable("dbo.Kategoriler");
        }
    }
}

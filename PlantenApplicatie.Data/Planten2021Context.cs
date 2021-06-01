using Microsoft.EntityFrameworkCore;
using PlantenApplicatie.Domain.Models;

namespace PlantenApplicatie.Data
{
    public partial class Planten2021Context : DbContext
    {
        public Planten2021Context()
        {
        }

        public Planten2021Context(DbContextOptions<Planten2021Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AbioBezonning> AbioBezonning { get; set; }
        public virtual DbSet<AbioGrondsoort> AbioGrondsoort { get; set; }
        public virtual DbSet<AbioHabitat> AbioHabitat { get; set; }
        public virtual DbSet<AbioReactieAntagonischeOmg> AbioReactieAntagonischeOmg { get; set; }
        public virtual DbSet<AbioVochtbehoefte> AbioVochtbehoefte { get; set; }
        public virtual DbSet<AbioVoedingsbehoefte> AbioVoedingsbehoefte { get; set; }
        public virtual DbSet<Abiotiek> Abiotiek { get; set; }
        public virtual DbSet<AbiotiekMulti> AbiotiekMulti { get; set; }
        public virtual DbSet<BeheerDaden> BeheerDaden { get; set; }
        public virtual DbSet<BeheerMaand> BeheerMaand { get; set; }
        public virtual DbSet<CommLevensvorm> CommLevensvorm { get; set; }
        public virtual DbSet<CommOntwikkelsnelheid> CommOntwikkelsnelheid { get; set; }
        public virtual DbSet<CommSocialbiliteit> CommSocialbiliteit { get; set; }
        public virtual DbSet<CommStrategie> CommStrategie { get; set; }
        public virtual DbSet<Commensalisme> Commensalisme { get; set; }
        public virtual DbSet<CommensalismeMulti> CommensalismeMulti { get; set; }
        public virtual DbSet<ExtraEigenschap> ExtraEigenschap { get; set; }
        public virtual DbSet<ExtraNectarwaarde> ExtraNectarwaarde { get; set; }
        public virtual DbSet<ExtraPollenwaarde> ExtraPollenwaarde { get; set; }
        public virtual DbSet<FenoBladgrootte> FenoBladgrootte { get; set; }
        public virtual DbSet<FenoBladvorm> FenoBladvorm { get; set; }
        public virtual DbSet<FenoBloeiwijze> FenoBloeiwijze { get; set; }
        public virtual DbSet<FenoHabitus> FenoHabitus { get; set; }
        public virtual DbSet<FenoKleur> FenoKleur { get; set; }
        public virtual DbSet<FenoLevensvorm> FenoLevensvorm { get; set; }
        public virtual DbSet<FenoMaand> FenoMaand { get; set; }
        public virtual DbSet<FenoRatioBloeiBlad> FenoRatioBloeiBlad { get; set; }
        public virtual DbSet<FenoSpruitfenologie> FenoSpruitfenologie { get; set; }
        public virtual DbSet<Fenotype> Fenotype { get; set; }
        public virtual DbSet<FenotypeMulti> FenotypeMulti { get; set; }
        public virtual DbSet<Foto> Foto { get; set; }
        public virtual DbSet<Gebruiker> Gebruiker { get; set; }
        public virtual DbSet<Plant> Plant { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<TfgsvFamilie> TfgsvFamilie { get; set; }
        public virtual DbSet<TfgsvGeslacht> TfgsvGeslacht { get; set; }
        public virtual DbSet<TfgsvSoort> TfgsvSoort { get; set; }
        public virtual DbSet<TfgsvType> TfgsvType { get; set; }
        public virtual DbSet<TfgsvVariant> TfgsvVariant { get; set; }
        public virtual DbSet<UpdatePlant> UpdatePlant { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(Constant.CONNECTION_STRING);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AbioBezonning>(entity =>
            {
                entity.ToTable("Abio_Bezonning");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Naam)
                    .HasColumnName("naam")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<AbioGrondsoort>(entity =>
            {
                entity.ToTable("Abio_Grondsoort");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Grondsoort)
                    .HasColumnName("grondsoort")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<AbioHabitat>(entity =>
            {
                entity.ToTable("Abio_Habitat");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Afkorting)
                    .HasColumnName("afkorting")
                    .HasMaxLength(10);

                entity.Property(e => e.Waarde)
                    .HasColumnName("waarde")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<AbioReactieAntagonischeOmg>(entity =>
            {
                entity.ToTable("Abio_Reactie_Antagonische_Omg");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Antagonie)
                    .HasColumnName("antagonie")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<AbioVochtbehoefte>(entity =>
            {
                entity.ToTable("Abio_Vochtbehoefte");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Vochtbehoefte)
                    .HasColumnName("vochtbehoefte")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<AbioVoedingsbehoefte>(entity =>
            {
                entity.ToTable("Abio_Voedingsbehoefte");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Voedingsbehoefte)
                    .HasColumnName("voedingsbehoefte")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Abiotiek>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AntagonischeOmgeving)
                    .HasColumnName("antagonischeOmgeving")
                    .HasMaxLength(50);

                entity.Property(e => e.Bezonning)
                    .HasColumnName("bezonning")
                    .HasMaxLength(30);

                entity.Property(e => e.Grondsoort)
                    .HasColumnName("grondsoort")
                    .HasMaxLength(3);

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.Property(e => e.Vochtbehoefte)
                    .HasColumnName("vochtbehoefte")
                    .HasMaxLength(30);

                entity.Property(e => e.Voedingsbehoefte)
                    .HasColumnName("voedingsbehoefte")
                    .HasMaxLength(20);

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Abiotiek)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("abiotiek_plant_FK");
            });

            modelBuilder.Entity<AbiotiekMulti>(entity =>
            {
                entity.ToTable("Abiotiek_Multi");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Eigenschap)
                    .HasColumnName("eigenschap")
                    .HasMaxLength(15);

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.Property(e => e.Waarde)
                    .HasColumnName("waarde")
                    .HasMaxLength(100);

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.AbiotiekMulti)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("commensalismev1_plant_FK");
            });

            modelBuilder.Entity<BeheerDaden>(entity =>
            {
                entity.ToTable("Beheer_Daden");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Beheerdaad)
                    .HasColumnName("beheerdaad")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BeheerMaand>(entity =>
            {
                entity.ToTable("Beheer_Maand");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apr).HasColumnName("apr");

                entity.Property(e => e.Aug).HasColumnName("aug");

                entity.Property(e => e.Beheerdaad)
                    .HasColumnName("beheerdaad")
                    .HasMaxLength(150);

                entity.Property(e => e.Dec).HasColumnName("dec");

                entity.Property(e => e.Feb).HasColumnName("feb");

                entity.Property(e => e.FrequentiePerJaar).HasColumnName("frequentie per jaar");

                entity.Property(e => e.Jan).HasColumnName("jan");

                entity.Property(e => e.Jul).HasColumnName("jul");

                entity.Property(e => e.Jun).HasColumnName("jun");

                entity.Property(e => e.M2u).HasColumnName("M2U");

                entity.Property(e => e.Mei).HasColumnName("mei");

                entity.Property(e => e.Mrt).HasColumnName("mrt");

                entity.Property(e => e.Nov).HasColumnName("nov");

                entity.Property(e => e.Okt).HasColumnName("okt");

                entity.Property(e => e.Omschrijving)
                    .HasColumnName("omschrijving")
                    .HasMaxLength(1000);

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.Property(e => e.Sept).HasColumnName("sept");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.BeheerMaand)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("beheer_maand_plant_FK");
            });

            modelBuilder.Entity<CommLevensvorm>(entity =>
            {
                entity.ToTable("Comm_Levensvorm");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Levensvorm)
                    .HasColumnName("levensvorm")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CommOntwikkelsnelheid>(entity =>
            {
                entity.ToTable("Comm_Ontwikkelsnelheid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Snelheid)
                    .HasColumnName("snelheid")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<CommSocialbiliteit>(entity =>
            {
                entity.ToTable("Comm_Socialbiliteit");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Sociabiliteit)
                    .HasColumnName("sociabiliteit")
                    .HasMaxLength(50);

                entity.Property(e => e.Waarde)
                    .HasColumnName("waarde")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CommStrategie>(entity =>
            {
                entity.ToTable("Comm_Strategie");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Strategie)
                    .HasColumnName("strategie")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Commensalisme>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Ontwikkelsnelheid)
                    .IsRequired()
                    .HasColumnName("ontwikkelsnelheid")
                    .HasMaxLength(10);

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.Property(e => e.Strategie)
                    .IsRequired()
                    .HasColumnName("strategie")
                    .HasMaxLength(10);

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Commensalisme)
                    .HasForeignKey(d => d.PlantId)
                    .HasConstraintName("FK_Commensalisme_Plant");
            });

            modelBuilder.Entity<CommensalismeMulti>(entity =>
            {
                entity.ToTable("Commensalisme_Multi");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Eigenschap)
                    .HasColumnName("eigenschap")
                    .HasMaxLength(50);

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.Property(e => e.Waarde)
                    .HasColumnName("waarde")
                    .HasMaxLength(200);

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.CommensalismeMulti)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("commensalisme_plant_FK");
            });

            modelBuilder.Entity<ExtraEigenschap>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bijvriendelijke).HasColumnName("bijvriendelijke");

                entity.Property(e => e.Eetbaar).HasColumnName("eetbaar");

                entity.Property(e => e.Geurend).HasColumnName("geurend");

                entity.Property(e => e.Kruidgebruik).HasColumnName("kruidgebruik");

                entity.Property(e => e.Nectarwaarde)
                    .HasColumnName("nectarwaarde")
                    .HasMaxLength(5);

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.Property(e => e.Pollenwaarde)
                    .HasColumnName("pollenwaarde")
                    .HasMaxLength(5);

                entity.Property(e => e.Vlindervriendelijk).HasColumnName("vlindervriendelijk");

                entity.Property(e => e.Vorstgevoelig).HasColumnName("vorstgevoelig");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.ExtraEigenschap)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ExtraEigenschap_plant_FK");
            });

            modelBuilder.Entity<ExtraNectarwaarde>(entity =>
            {
                entity.ToTable("Extra_Nectarwaarde");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Waarde)
                    .HasColumnName("waarde")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<ExtraPollenwaarde>(entity =>
            {
                entity.ToTable("Extra_Pollenwaarde");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Waarde)
                    .HasColumnName("waarde")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<FenoBladgrootte>(entity =>
            {
                entity.ToTable("Feno_Bladgrootte");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bladgrootte)
                    .HasColumnName("bladgrootte")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<FenoBladvorm>(entity =>
            {
                entity.ToTable("Feno_Bladvorm");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Vorm)
                    .HasColumnName("vorm")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<FenoBloeiwijze>(entity =>
            {
                entity.ToTable("Feno_Bloeiwijze");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Figuur)
                    .HasColumnName("figuur")
                    .HasColumnType("image");

                entity.Property(e => e.Naam)
                    .HasColumnName("naam")
                    .HasMaxLength(20);

                entity.Property(e => e.UrlLocatie)
                    .HasColumnName("url/locatie")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<FenoHabitus>(entity =>
            {
                entity.ToTable("Feno_Habitus");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Figuur)
                    .HasColumnName("figuur")
                    .HasColumnType("image");

                entity.Property(e => e.Naam)
                    .HasColumnName("naam")
                    .HasMaxLength(30);

                entity.Property(e => e.UrlLocatie)
                    .HasColumnName("url/locatie")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<FenoKleur>(entity =>
            {
                entity.ToTable("Feno_Kleur");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.HexWaarde)
                    .HasColumnName("hex_waarde")
                    .HasMaxLength(3);

                entity.Property(e => e.NaamKleur)
                    .HasColumnName("naam kleur")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<FenoLevensvorm>(entity =>
            {
                entity.ToTable("Feno_Levensvorm");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Figuur)
                    .HasColumnName("figuur")
                    .HasColumnType("image");

                entity.Property(e => e.Levensvorm)
                    .HasColumnName("levensvorm")
                    .HasMaxLength(100);

                entity.Property(e => e.UrlLocatie)
                    .HasColumnName("url/locatie")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<FenoMaand>(entity =>
            {
                entity.ToTable("Feno_Maand");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Maand)
                    .HasColumnName("maand")
                    .HasMaxLength(3);
            });

            modelBuilder.Entity<FenoRatioBloeiBlad>(entity =>
            {
                entity.ToTable("Feno_RatioBloeiBlad");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Waarde)
                    .HasColumnName("waarde")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<FenoSpruitfenologie>(entity =>
            {
                entity.ToTable("Feno_Spruitfenologie");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Fenologie)
                    .HasColumnName("fenologie")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Fenotype>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bladgrootte).HasColumnName("bladgrootte");

                entity.Property(e => e.Bladvorm)
                    .HasColumnName("bladvorm")
                    .HasMaxLength(50);

                entity.Property(e => e.Bloeiwijze)
                    .HasColumnName("bloeiwijze")
                    .HasMaxLength(20);

                entity.Property(e => e.Habitus)
                    .HasColumnName("habitus")
                    .HasMaxLength(20);

                entity.Property(e => e.Levensvorm)
                    .HasColumnName("levensvorm")
                    .HasMaxLength(50);

                entity.Property(e => e.MaxBladhoogte).HasColumnName("maxBladhoogte");

                entity.Property(e => e.MaxBloeihoogte).HasColumnName("maxBloeihoogte");

                entity.Property(e => e.MinBloeihoogte).HasColumnName("minBloeihoogte");

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.Property(e => e.RatioBloeiBlad)
                    .HasColumnName("ratioBloeiBlad")
                    .HasMaxLength(50);

                entity.Property(e => e.Spruitfenologie)
                    .HasColumnName("spruitfenologie")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.Fenotype)
                    .HasForeignKey(d => d.PlantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fenotype_plant_FK");
            });

            modelBuilder.Entity<FenotypeMulti>(entity =>
            {
                entity.ToTable("Fenotype_Multi");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Eigenschap)
                    .HasColumnName("eigenschap")
                    .HasMaxLength(50);

                entity.Property(e => e.Maand)
                    .HasColumnName("maand")
                    .HasMaxLength(10);

                entity.Property(e => e.PlantId).HasColumnName("plant_id");

                entity.Property(e => e.Waarde)
                    .HasColumnName("waarde")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Foto>(entity =>
            {
                entity.Property(e => e.Fotoid).HasColumnName("fotoid");

                entity.Property(e => e.Eigenschap)
                    .HasColumnName("eigenschap")
                    .HasMaxLength(20);

                entity.Property(e => e.Plant).HasColumnName("plant");

                entity.Property(e => e.Tumbnail)
                    .HasColumnName("tumbnail")
                    .HasColumnType("image");

                entity.Property(e => e.UrlLocatie)
                    .HasColumnName("url/locatie")
                    .HasMaxLength(500);

                entity.HasOne(d => d.PlantNavigation)
                    .WithMany(p => p.Foto)
                    .HasForeignKey(d => d.Plant)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("foto_plant_FK");
            });

            modelBuilder.Entity<Gebruiker>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Achternaam)
                    .HasColumnName("achternaam")
                    .HasMaxLength(50);

                entity.Property(e => e.Emailadres)
                    .HasColumnName("emailadres")
                    .HasMaxLength(150);

                entity.Property(e => e.HashPaswoord)
                    .HasColumnName("hashPaswoord")
                    .HasMaxLength(64);

                entity.Property(e => e.LastLogin)
                    .HasColumnName("last_login")
                    .HasColumnType("date");

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasColumnName("rol")
                    .HasMaxLength(20);

                entity.Property(e => e.Vivesnr)
                    .HasColumnName("vivesnr")
                    .HasMaxLength(15);

                entity.Property(e => e.Voornaam)
                    .HasColumnName("voornaam")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Plant>(entity =>
            {
                entity.Property(e => e.PlantId).HasColumnName("plant-id");

                entity.Property(e => e.Familie)
                    .HasColumnName("familie")
                    .HasMaxLength(100);

                entity.Property(e => e.FamilieId).HasColumnName("familieID");

                entity.Property(e => e.Fgsv)
                    .HasColumnName("fgsv")
                    .HasMaxLength(500);

                entity.Property(e => e.Geslacht)
                    .HasColumnName("geslacht")
                    .HasMaxLength(100);

                entity.Property(e => e.GeslachtId).HasColumnName("geslachtID");

                entity.Property(e => e.IdAccess).HasColumnName("idAccess");

                entity.Property(e => e.NederlandsNaam)
                    .HasColumnName("nederlands naam")
                    .HasMaxLength(500);

                entity.Property(e => e.PlantdichtheidMax).HasColumnName("plantdichtheid_max");

                entity.Property(e => e.PlantdichtheidMin).HasColumnName("plantdichtheid_min");

                entity.Property(e => e.Soort)
                    .HasColumnName("soort")
                    .HasMaxLength(100);

                entity.Property(e => e.SoortId).HasColumnName("soortID");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(100);

                entity.Property(e => e.TypeId).HasColumnName("typeID");

                entity.Property(e => e.Variant)
                    .HasColumnName("variant")
                    .HasMaxLength(100);

                entity.Property(e => e.VariantId).HasColumnName("variantID");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Omschrijving)
                    .HasColumnName("omschrijving")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<TfgsvFamilie>(entity =>
            {
                entity.HasKey(e => e.FamileId)
                    .HasName("familie_PK");

                entity.ToTable("Tfgsv_Familie");

                entity.Property(e => e.FamileId).HasColumnName("famile_id");

                entity.Property(e => e.Familienaam)
                    .HasColumnName("familienaam")
                    .HasMaxLength(100);

                entity.Property(e => e.NlNaam)
                    .HasColumnName("NL naam")
                    .HasMaxLength(100);

                entity.Property(e => e.TypeTypeid).HasColumnName("type_typeid");

                entity.HasOne(d => d.TypeType)
                    .WithMany(p => p.TfgsvFamilie)
                    .HasForeignKey(d => d.TypeTypeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("familie_type_FK");
            });

            modelBuilder.Entity<TfgsvGeslacht>(entity =>
            {
                entity.HasKey(e => e.GeslachtId)
                    .HasName("geslacht_PK");

                entity.ToTable("Tfgsv_Geslacht");

                entity.Property(e => e.GeslachtId).HasColumnName("geslacht_id");

                entity.Property(e => e.FamilieFamileId).HasColumnName("familie_famile_id");

                entity.Property(e => e.Geslachtnaam)
                    .HasColumnName("geslachtnaam")
                    .HasMaxLength(100);

                entity.Property(e => e.NlNaam)
                    .HasColumnName("NL naam")
                    .HasMaxLength(100);

                entity.HasOne(d => d.FamilieFamile)
                    .WithMany(p => p.TfgsvGeslacht)
                    .HasForeignKey(d => d.FamilieFamileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("geslacht_familie_FK");
            });

            modelBuilder.Entity<TfgsvSoort>(entity =>
            {
                entity.HasKey(e => e.Soortid)
                    .HasName("soort_PK");

                entity.ToTable("Tfgsv_Soort");

                entity.Property(e => e.Soortid).HasColumnName("soortid");

                entity.Property(e => e.GeslachtGeslachtId).HasColumnName("geslacht_geslacht_id");

                entity.Property(e => e.NlNaam)
                    .HasColumnName("NL naam")
                    .HasMaxLength(100);

                entity.Property(e => e.Soortnaam)
                    .HasColumnName("soortnaam")
                    .HasMaxLength(100);

                entity.HasOne(d => d.GeslachtGeslacht)
                    .WithMany(p => p.TfgsvSoort)
                    .HasForeignKey(d => d.GeslachtGeslachtId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("soort_geslacht_FK");
            });

            modelBuilder.Entity<TfgsvType>(entity =>
            {
                entity.HasKey(e => e.Planttypeid)
                    .HasName("type_PK");

                entity.ToTable("Tfgsv_Type");

                entity.Property(e => e.Planttypeid).HasColumnName("planttypeid");

                entity.Property(e => e.Planttypenaam)
                    .HasColumnName("planttypenaam")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TfgsvVariant>(entity =>
            {
                entity.HasKey(e => e.VariantId)
                    .HasName("variant_PK");

                entity.ToTable("Tfgsv_Variant");

                entity.Property(e => e.VariantId).HasColumnName("variantID");

                entity.Property(e => e.NlNaam)
                    .HasColumnName("NL naam")
                    .HasMaxLength(100);

                entity.Property(e => e.SoortSoortid).HasColumnName("soort_soortid");

                entity.Property(e => e.Variantnaam)
                    .HasColumnName("variantnaam")
                    .HasMaxLength(100);

                entity.HasOne(d => d.SoortSoort)
                    .WithMany(p => p.TfgsvVariant)
                    .HasForeignKey(d => d.SoortSoortid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tfgsv_Variant_Tfgsv_Soort");
            });

            modelBuilder.Entity<UpdatePlant>(entity =>
            {
                entity.ToTable("Update_Plant");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Plantid).HasColumnName("plantid");

                entity.Property(e => e.Updatedatum)
                    .HasColumnName("updatedatum")
                    .HasColumnType("datetime");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Plant)
                    .WithMany(p => p.UpdatePlant)
                    .HasForeignKey(d => d.Plantid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("update_plant_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UpdatePlant)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("update_users_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

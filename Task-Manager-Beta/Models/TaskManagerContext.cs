using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Task_Manager_Beta.Data;

namespace Task_Manager_Beta.Data;

public partial class TaskManagerContext : DbContext
{
    public TaskManagerContext()
    {
    }

    public TaskManagerContext(DbContextOptions<TaskManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<Condition> Conditions { get; set; }

    public virtual DbSet<Listtemplate> Listtemplates { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Permisssion> Permisssions { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<Taskdetail> Taskdetails { get; set; }

    public virtual DbSet<Template> Templates { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Workflow> Workflows { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer
    ("Server=MAR03\\SQLEXPRESS;Database=TaskManager;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => new { e.Iduser, e.Idtask }).HasName("PK__ASSIGNME__612AE3C007018D2B");

            entity.ToTable("ASSIGNMENT");

            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.Idtask).HasColumnName("IDTask");
            entity.Property(e => e.Idassignment)
                .ValueGeneratedOnAdd()
                .HasColumnName("IDAssignment");

            entity.HasOne(d => d.IdtaskNavigation).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.Idtask)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ASSIGNMEN__IDTas__656C112C");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.Iduser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ASSIGNMEN__IDUse__6477ECF3");
        });

        modelBuilder.Entity<Condition>(entity =>
        {
            entity.HasKey(e => e.Idcondition).HasName("PK__CONDITIO__452B4D93615D6001");

            entity.ToTable("CONDITION");

            entity.Property(e => e.Idcondition).HasColumnName("IDCondition");
            entity.Property(e => e.Idpermission).HasColumnName("IDPermission");
            entity.Property(e => e.Idworkflow).HasColumnName("IDWorkflow");

            entity.HasOne(d => d.IdpermissionNavigation).WithMany(p => p.Conditions)
                .HasForeignKey(d => d.Idpermission)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CONDITION__IDPer__693CA210");

            entity.HasOne(d => d.IdworkflowNavigation).WithMany(p => p.Conditions)
                .HasForeignKey(d => d.Idworkflow)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CONDITION__IDWor__6A30C649");
        });

        modelBuilder.Entity<Listtemplate>(entity =>
        {
            entity.HasKey(e => e.IdlistTemplate).HasName("PK__LISTTEMP__55547DE54957B17C");

            entity.ToTable("LISTTEMPLATE");

            entity.Property(e => e.IdlistTemplate).HasColumnName("IDListTemplate");
            entity.Property(e => e.Idtemplate).HasColumnName("IDTemplate");
            entity.Property(e => e.StatusName).HasMaxLength(50);

            entity.HasOne(d => d.IdtemplateNavigation).WithMany(p => p.Listtemplates)
                .HasForeignKey(d => d.Idtemplate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LISTTEMPL__IDTem__6B24EA82");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Idmember).HasName("PK__MEMBER__7EB75A63B9ADC7C1");

            entity.ToTable("MEMBER");

            entity.Property(e => e.Idmember).HasColumnName("IDMember");
            entity.Property(e => e.Idproject).HasColumnName("IDProject");
            entity.Property(e => e.Iduser).HasColumnName("IDUser");

            entity.HasOne(d => d.IdprojectNavigation).WithMany(p => p.Members)
                .HasForeignKey(d => d.Idproject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MEMBER__IDProjec__6383C8BA");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Members)
                .HasForeignKey(d => d.Iduser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MEMBER__IDUser__628FA481");
        });

        modelBuilder.Entity<Permisssion>(entity =>
        {
            entity.HasKey(e => e.Idpermission).HasName("PK__PERMISSS__0553C49A071A4B4F");

            entity.ToTable("PERMISSSION");

            entity.Property(e => e.Idpermission).HasColumnName("IDPermission");
            entity.Property(e => e.Idproject).HasColumnName("IDProject");
            entity.Property(e => e.Iduser).HasColumnName("IDUser");

            entity.HasOne(d => d.IdprojectNavigation).WithMany(p => p.Permisssions)
                .HasForeignKey(d => d.Idproject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PERMISSSI__IDPro__6754599E");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Permisssions)
                .HasForeignKey(d => d.Iduser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PERMISSSI__IDUse__66603565");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Idproject).HasName("PK__PROJECT__B0529955FD19BFEF");

            entity.ToTable("PROJECT");

            entity.Property(e => e.Idproject).HasColumnName("IDProject");
            entity.Property(e => e.DayCreate).HasColumnType("datetime");
            entity.Property(e => e.Idleader).HasColumnName("IDLeader");
            entity.Property(e => e.ProjectName).HasMaxLength(50);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Idstatus).HasName("PK__STATUS__8DA24510AF0F9F38");

            entity.ToTable("STATUS");

            entity.Property(e => e.Idstatus).HasColumnName("IDStatus");
            entity.Property(e => e.Idproject).HasColumnName("IDProject");
            entity.Property(e => e.StatusName).HasMaxLength(50);

            entity.HasOne(d => d.IdprojectNavigation).WithMany(p => p.Statuses)
                .HasForeignKey(d => d.Idproject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__STATUS__IDProjec__6C190EBB");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Idtask).HasName("PK__TASK__BCC3A1F9504858B2");

            entity.ToTable("TASK");

            entity.Property(e => e.Idtask).HasColumnName("IDTask");
            entity.Property(e => e.DayCreate).HasColumnType("datetime");
            entity.Property(e => e.DayStart).HasColumnType("datetime");
            entity.Property(e => e.Deadline).HasColumnType("datetime");
            entity.Property(e => e.Idproject).HasColumnName("IDProject");
            entity.Property(e => e.Idstatus).HasColumnName("IDStatus");
            entity.Property(e => e.TaskName).HasMaxLength(50);

            entity.HasOne(d => d.IdprojectNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.Idproject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TASK__IDProject__5FB337D6");

            entity.HasOne(d => d.IdstatusNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.Idstatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TASK__IDStatus__60A75C0F");
        });

        modelBuilder.Entity<Taskdetail>(entity =>
        {
            entity.HasKey(e => e.Idtask).HasName("PK__TASKDETA__BCC3A1F9E297B0EB");

            entity.ToTable("TASKDETAILS");

            entity.Property(e => e.Idtask)
                .ValueGeneratedOnAdd()
                .HasColumnName("IDTask");

            entity.HasOne(d => d.IdtaskNavigation).WithOne(p => p.Taskdetail)
                .HasForeignKey<Taskdetail>(d => d.Idtask)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TASKDETAI__IDTas__619B8048");
        });

        modelBuilder.Entity<Template>(entity =>
        {
            entity.HasKey(e => e.Idtemplate).HasName("PK__TEMPLATE__AB4388EFB0D1E74B");

            entity.ToTable("TEMPLATE");

            entity.HasIndex(e => e.TemplateName, "UQ__TEMPLATE__A6C2DA6656661883").IsUnique();

            entity.Property(e => e.Idtemplate).HasColumnName("IDTemplate");
            entity.Property(e => e.TemplateName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser).HasName("PK__USER__EAE6D9DFA1CC6D4F");

            entity.ToTable("USER");

            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.Avatar).IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password).IsUnicode(false);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<Workflow>(entity =>
        {
            entity.HasKey(e => e.Idworkflow).HasName("PK__WORKFLOW__7D45E40A469AE309");

            entity.ToTable("WORKFLOW");

            entity.Property(e => e.Idworkflow).HasColumnName("IDWorkflow");
            entity.Property(e => e.Idstatus).HasColumnName("IDStatus");

            entity.HasOne(d => d.IdstatusNavigation).WithMany(p => p.Workflows)
                .HasForeignKey(d => d.Idstatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__WORKFLOW__IDStat__68487DD7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

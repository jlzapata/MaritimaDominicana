namespace MaritimaDominicana.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Telephone = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.ProblemDetails",
                c => new
                    {
                        ProblemDetailId = c.Int(nullable: false, identity: true),
                        ProblemId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100, unicode: false),
                        DepartmentId = c.Int(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 200, unicode: false),
                        Date = c.DateTime(nullable: false),
                        PlaceId = c.Int(nullable: false),
                        Update_at = c.DateTime(),
                        Modified_by = c.Int(),
                        AssignedTo = c.Int(),
                        AssignedAt = c.DateTime(),
                        state = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProblemDetailId)
                .ForeignKey("dbo.Users", t => t.AssignedTo)
                .ForeignKey("dbo.Users", t => t.CreatedBy)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.Places", t => t.PlaceId)
                .ForeignKey("dbo.Problems", t => t.ProblemId)
                .Index(t => t.ProblemId)
                .Index(t => t.DepartmentId)
                .Index(t => t.CreatedBy)
                .Index(t => t.PlaceId)
                .Index(t => t.AssignedTo)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50, unicode: false),
                        LastName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Pasword = c.String(nullable: false, unicode: false),
                        Email = c.String(nullable: false, maxLength: 100, unicode: false),
                        TypeId = c.Int(nullable: false),
                        Active = c.Boolean(),
                        Connected = c.Boolean(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Types", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.Email, unique: true)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.Solutions",
                c => new
                    {
                        SolutionId = c.Int(nullable: false),
                        SolutionDescription = c.String(nullable: false, unicode: false),
                        UserId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SolutionId)
                .ForeignKey("dbo.ProblemDetails", t => t.SolutionId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.SolutionId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        TypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.TypeId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        PlaceId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PlaceId);
            
            CreateTable(
                "dbo.Problems",
                c => new
                    {
                        ProblemId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ProblemId);
            
            CreateTable(
                "dbo.Followers",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        FollowerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.FollowerId })
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Users", t => t.FollowerId)
                .Index(t => t.UserId)
                .Index(t => t.FollowerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProblemDetails", "ProblemId", "dbo.Problems");
            DropForeignKey("dbo.ProblemDetails", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.ProblemDetails", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ProblemDetails", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Users", "TypeId", "dbo.Types");
            DropForeignKey("dbo.Solutions", "UserId", "dbo.Users");
            DropForeignKey("dbo.Solutions", "SolutionId", "dbo.ProblemDetails");
            DropForeignKey("dbo.ProblemDetails", "CreatedBy", "dbo.Users");
            DropForeignKey("dbo.Followers", "FollowerId", "dbo.Users");
            DropForeignKey("dbo.Followers", "UserId", "dbo.Users");
            DropForeignKey("dbo.ProblemDetails", "AssignedTo", "dbo.Users");
            DropIndex("dbo.Followers", new[] { "FollowerId" });
            DropIndex("dbo.Followers", new[] { "UserId" });
            DropIndex("dbo.Solutions", new[] { "UserId" });
            DropIndex("dbo.Solutions", new[] { "SolutionId" });
            DropIndex("dbo.Users", new[] { "TypeId" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.ProblemDetails", new[] { "ClientId" });
            DropIndex("dbo.ProblemDetails", new[] { "AssignedTo" });
            DropIndex("dbo.ProblemDetails", new[] { "PlaceId" });
            DropIndex("dbo.ProblemDetails", new[] { "CreatedBy" });
            DropIndex("dbo.ProblemDetails", new[] { "DepartmentId" });
            DropIndex("dbo.ProblemDetails", new[] { "ProblemId" });
            DropTable("dbo.Followers");
            DropTable("dbo.Problems");
            DropTable("dbo.Places");
            DropTable("dbo.Departments");
            DropTable("dbo.Types");
            DropTable("dbo.Solutions");
            DropTable("dbo.Users");
            DropTable("dbo.ProblemDetails");
            DropTable("dbo.Clients");
        }
    }
}

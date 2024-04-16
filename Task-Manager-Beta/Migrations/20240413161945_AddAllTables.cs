using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task_Manager_Beta.Migrations
{
    /// <inheritdoc />
    public partial class AddAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PROJECT",
                columns: table => new
                {
                    IDProject = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IDLeader = table.Column<int>(type: "int", nullable: true),
                    DayCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hide = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PROJECT__B0529955FD19BFEF", x => x.IDProject);
                });

            migrationBuilder.CreateTable(
                name: "TEMPLATE",
                columns: table => new
                {
                    IDTemplate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TEMPLATE__AB4388EFB0D1E74B", x => x.IDTemplate);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    IDUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Avatar = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Hide = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__USER__EAE6D9DFA1CC6D4F", x => x.IDUser);
                });

            migrationBuilder.CreateTable(
                name: "STATUS",
                columns: table => new
                {
                    IDStatus = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDProject = table.Column<int>(type: "int", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__STATUS__8DA24510AF0F9F38", x => x.IDStatus);
                    table.ForeignKey(
                        name: "FK__STATUS__IDProjec__6C190EBB",
                        column: x => x.IDProject,
                        principalTable: "PROJECT",
                        principalColumn: "IDProject");
                });

            migrationBuilder.CreateTable(
                name: "LISTTEMPLATE",
                columns: table => new
                {
                    IDListTemplate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDTemplate = table.Column<int>(type: "int", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LISTTEMP__55547DE54957B17C", x => x.IDListTemplate);
                    table.ForeignKey(
                        name: "FK__LISTTEMPL__IDTem__6B24EA82",
                        column: x => x.IDTemplate,
                        principalTable: "TEMPLATE",
                        principalColumn: "IDTemplate");
                });

            migrationBuilder.CreateTable(
                name: "MEMBER",
                columns: table => new
                {
                    IDMember = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDUser = table.Column<int>(type: "int", nullable: false),
                    IDProject = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MEMBER__7EB75A63B9ADC7C1", x => x.IDMember);
                    table.ForeignKey(
                        name: "FK__MEMBER__IDProjec__6383C8BA",
                        column: x => x.IDProject,
                        principalTable: "PROJECT",
                        principalColumn: "IDProject");
                    table.ForeignKey(
                        name: "FK__MEMBER__IDUser__628FA481",
                        column: x => x.IDUser,
                        principalTable: "USER",
                        principalColumn: "IDUser");
                });

            migrationBuilder.CreateTable(
                name: "PERMISSSION",
                columns: table => new
                {
                    IDPermission = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDUser = table.Column<int>(type: "int", nullable: false),
                    IDProject = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Object = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Privilege = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PERMISSS__0553C49A071A4B4F", x => x.IDPermission);
                    table.ForeignKey(
                        name: "FK__PERMISSSI__IDPro__6754599E",
                        column: x => x.IDProject,
                        principalTable: "PROJECT",
                        principalColumn: "IDProject");
                    table.ForeignKey(
                        name: "FK__PERMISSSI__IDUse__66603565",
                        column: x => x.IDUser,
                        principalTable: "USER",
                        principalColumn: "IDUser");
                });

            migrationBuilder.CreateTable(
                name: "TASK",
                columns: table => new
                {
                    IDTask = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDProject = table.Column<int>(type: "int", nullable: false),
                    IDStatus = table.Column<int>(type: "int", nullable: false),
                    TaskName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DayCreate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DayStart = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deadline = table.Column<DateTime>(type: "datetime", nullable: true),
                    Hide = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TASK__BCC3A1F9504858B2", x => x.IDTask);
                    table.ForeignKey(
                        name: "FK__TASK__IDProject__5FB337D6",
                        column: x => x.IDProject,
                        principalTable: "PROJECT",
                        principalColumn: "IDProject");
                    table.ForeignKey(
                        name: "FK__TASK__IDStatus__60A75C0F",
                        column: x => x.IDStatus,
                        principalTable: "STATUS",
                        principalColumn: "IDStatus");
                });

            migrationBuilder.CreateTable(
                name: "WORKFLOW",
                columns: table => new
                {
                    IDWorkflow = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDStatus = table.Column<int>(type: "int", nullable: false),
                    Transition = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WORKFLOW__7D45E40A469AE309", x => x.IDWorkflow);
                    table.ForeignKey(
                        name: "FK__WORKFLOW__IDStat__68487DD7",
                        column: x => x.IDStatus,
                        principalTable: "STATUS",
                        principalColumn: "IDStatus");
                });

            migrationBuilder.CreateTable(
                name: "ASSIGNMENT",
                columns: table => new
                {
                    IDUser = table.Column<int>(type: "int", nullable: false),
                    IDTask = table.Column<int>(type: "int", nullable: false),
                    IDAssignment = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ASSIGNME__612AE3C007018D2B", x => new { x.IDUser, x.IDTask });
                    table.ForeignKey(
                        name: "FK__ASSIGNMEN__IDTas__656C112C",
                        column: x => x.IDTask,
                        principalTable: "TASK",
                        principalColumn: "IDTask");
                    table.ForeignKey(
                        name: "FK__ASSIGNMEN__IDUse__6477ECF3",
                        column: x => x.IDUser,
                        principalTable: "USER",
                        principalColumn: "IDUser");
                });

            migrationBuilder.CreateTable(
                name: "TASKDETAILS",
                columns: table => new
                {
                    IDTask = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Attachments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TASKDETA__BCC3A1F9E297B0EB", x => x.IDTask);
                    table.ForeignKey(
                        name: "FK__TASKDETAI__IDTas__619B8048",
                        column: x => x.IDTask,
                        principalTable: "TASK",
                        principalColumn: "IDTask");
                });

            migrationBuilder.CreateTable(
                name: "CONDITION",
                columns: table => new
                {
                    IDCondition = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDPermission = table.Column<int>(type: "int", nullable: false),
                    IDWorkflow = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CONDITIO__452B4D93615D6001", x => x.IDCondition);
                    table.ForeignKey(
                        name: "FK__CONDITION__IDPer__693CA210",
                        column: x => x.IDPermission,
                        principalTable: "PERMISSSION",
                        principalColumn: "IDPermission");
                    table.ForeignKey(
                        name: "FK__CONDITION__IDWor__6A30C649",
                        column: x => x.IDWorkflow,
                        principalTable: "WORKFLOW",
                        principalColumn: "IDWorkflow");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ASSIGNMENT_IDTask",
                table: "ASSIGNMENT",
                column: "IDTask");

            migrationBuilder.CreateIndex(
                name: "IX_CONDITION_IDPermission",
                table: "CONDITION",
                column: "IDPermission");

            migrationBuilder.CreateIndex(
                name: "IX_CONDITION_IDWorkflow",
                table: "CONDITION",
                column: "IDWorkflow");

            migrationBuilder.CreateIndex(
                name: "IX_LISTTEMPLATE_IDTemplate",
                table: "LISTTEMPLATE",
                column: "IDTemplate");

            migrationBuilder.CreateIndex(
                name: "IX_MEMBER_IDProject",
                table: "MEMBER",
                column: "IDProject");

            migrationBuilder.CreateIndex(
                name: "IX_MEMBER_IDUser",
                table: "MEMBER",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_PERMISSSION_IDProject",
                table: "PERMISSSION",
                column: "IDProject");

            migrationBuilder.CreateIndex(
                name: "IX_PERMISSSION_IDUser",
                table: "PERMISSSION",
                column: "IDUser");

            migrationBuilder.CreateIndex(
                name: "IX_STATUS_IDProject",
                table: "STATUS",
                column: "IDProject");

            migrationBuilder.CreateIndex(
                name: "IX_TASK_IDProject",
                table: "TASK",
                column: "IDProject");

            migrationBuilder.CreateIndex(
                name: "IX_TASK_IDStatus",
                table: "TASK",
                column: "IDStatus");

            migrationBuilder.CreateIndex(
                name: "UQ__TEMPLATE__A6C2DA6656661883",
                table: "TEMPLATE",
                column: "TemplateName",
                unique: true,
                filter: "[TemplateName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_WORKFLOW_IDStatus",
                table: "WORKFLOW",
                column: "IDStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ASSIGNMENT");

            migrationBuilder.DropTable(
                name: "CONDITION");

            migrationBuilder.DropTable(
                name: "LISTTEMPLATE");

            migrationBuilder.DropTable(
                name: "MEMBER");

            migrationBuilder.DropTable(
                name: "TASKDETAILS");

            migrationBuilder.DropTable(
                name: "PERMISSSION");

            migrationBuilder.DropTable(
                name: "WORKFLOW");

            migrationBuilder.DropTable(
                name: "TEMPLATE");

            migrationBuilder.DropTable(
                name: "TASK");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "STATUS");

            migrationBuilder.DropTable(
                name: "PROJECT");
        }
    }
}

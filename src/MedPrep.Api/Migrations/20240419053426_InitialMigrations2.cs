using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedPrep.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Playlist",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlist", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseModule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Topic = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Collection = table.Column<Guid>(type: "uuid", nullable: true),
                    PlaylistId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseModule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseModule_Playlist_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlist",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "License",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    ProvinceOrState = table.Column<string>(type: "text", nullable: false),
                    LicenseNumber = table.Column<string>(type: "text", nullable: false),
                    LicenseName = table.Column<string>(type: "text", nullable: false),
                    AccreditationType = table.Column<string>(type: "text", nullable: false),
                    LicenseSource = table.Column<string>(type: "text", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_License", x => x.Id);
                    table.ForeignKey(
                        name: "FK_License_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TeacherId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CourseModuleId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Video_CourseModule_CourseModuleId",
                        column: x => x.CourseModuleId,
                        principalTable: "CourseModule",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Video_Teacher_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubtitleSource",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Source = table.Column<string>(type: "text", nullable: false),
                    Language = table.Column<string>(type: "text", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    VideoId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubtitleSource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubtitleSource_Video_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Video",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserVideo",
                columns: table => new
                {
                    PurchasersId = table.Column<Guid>(type: "uuid", nullable: false),
                    VideoPurchasesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVideo", x => new { x.PurchasersId, x.VideoPurchasesId });
                    table.ForeignKey(
                        name: "FK_UserVideo_User_PurchasersId",
                        column: x => x.PurchasersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserVideo_Video_VideoPurchasesId",
                        column: x => x.VideoPurchasesId,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoSource",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Source = table.Column<string>(type: "text", nullable: false),
                    VideoId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoSource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoSource_Video_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseModule_IsDeleted",
                table: "CourseModule",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_CourseModule_PlaylistId",
                table: "CourseModule",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_License_IsDeleted",
                table: "License",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_License_TeacherId",
                table: "License",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_IsDeleted",
                table: "Playlist",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_SubtitleSource_IsDeleted",
                table: "SubtitleSource",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_SubtitleSource_VideoId",
                table: "SubtitleSource",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_IsDeleted",
                table: "Teacher",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_IsDeleted",
                table: "User",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_UserVideo_VideoPurchasesId",
                table: "UserVideo",
                column: "VideoPurchasesId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_CourseModuleId",
                table: "Video",
                column: "CourseModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_IsDeleted",
                table: "Video",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Video_TeacherId",
                table: "Video",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoSource_IsDeleted",
                table: "VideoSource",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_VideoSource_VideoId",
                table: "VideoSource",
                column: "VideoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "License");

            migrationBuilder.DropTable(
                name: "SubtitleSource");

            migrationBuilder.DropTable(
                name: "UserVideo");

            migrationBuilder.DropTable(
                name: "VideoSource");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "CourseModule");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "Playlist");
        }
    }
}

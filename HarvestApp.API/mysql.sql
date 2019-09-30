﻿CREATE TABLE "__EFMigrationsHistory" (
    "MigrationId" varchar(95) NOT NULL, CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" varchar(32) NOT NULL
);

CREATE TABLE "Users" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Users" PRIMARY KEY,
    "Username" TEXT NULL,
    "PasswordHash" BLOB NULL,
    "PasswordSalt" BLOB NULL,
    "Gender" TEXT NULL,
    "DateOfBirth" TEXT NOT NULL,
    "KnownAs" TEXT NULL,
    "Created" TEXT NOT NULL,
    "LastActive" TEXT NOT NULL,
    "Introduction" TEXT NULL,
    "LookingFor" TEXT NULL,
    "Interests" TEXT NULL,
    "City" TEXT NULL,
    "Country" TEXT NULL
);

CREATE TABLE "Values" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Values" PRIMARY KEY,
    "Name" TEXT NULL
);

CREATE TABLE "Likes" (
    "LikerId" INTEGER NOT NULL,
    "LikeeId" INTEGER NOT NULL,
    CONSTRAINT "PK_Likes" PRIMARY KEY ("LikerId", "LikeeId"),
    CONSTRAINT "FK_Likes_Users_LikeeId" FOREIGN KEY ("LikeeId") REFERENCES "Users" ("Id") ON DELETE NO ACTION,
    CONSTRAINT "FK_Likes_Users_LikerId" FOREIGN KEY ("LikerId") REFERENCES "Users" ("Id") ON DELETE NO ACTION
);

CREATE TABLE "Messages" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Messages" PRIMARY KEY,
    "SenderId" INTEGER NOT NULL,
    "RecipientId" INTEGER NOT NULL,
    "Content" TEXT NULL,
    "IsRead" INTEGER NOT NULL,
    "DateRead" TEXT NULL,
    "MessageSent" TEXT NOT NULL,
    "SenderDeleted" INTEGER NOT NULL,
    "RecipientDeleted" INTEGER NOT NULL,
    CONSTRAINT "FK_Messages_Users_RecipientId" FOREIGN KEY ("RecipientId") REFERENCES "Users" ("Id") ON DELETE NO ACTION,
    CONSTRAINT "FK_Messages_Users_SenderId" FOREIGN KEY ("SenderId") REFERENCES "Users" ("Id") ON DELETE NO ACTION
);

CREATE TABLE "Photos" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Photos" PRIMARY KEY,
    "Url" TEXT NULL,
    "Description" TEXT NULL,
    "DateAdded" TEXT NOT NULL,
    "IsMain" INTEGER NOT NULL,
    "PublicId" TEXT NULL,
    "UserId" INTEGER NOT NULL,
    CONSTRAINT "FK_Photos_Users_UserId" FOREIGN KEY ("UserId") REFERENCES "Users" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Likes_LikeeId" ON "Likes" ("LikeeId");

CREATE INDEX "IX_Messages_RecipientId" ON "Messages" ("RecipientId");

CREATE INDEX "IX_Messages_SenderId" ON "Messages" ("SenderId");

CREATE INDEX "IX_Photos_UserId" ON "Photos" ("UserId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20190522024500_MySqlInit', '2.1.8-servicing-32085');


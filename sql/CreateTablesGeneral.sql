CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Fornecedores" (
    "Id" uuid NOT NULL,
    "Name" varchar(100) NOT NULL,
    "Document" varchar(14) NOT NULL,
    "TypeProviders" integer NOT NULL,
    "Active" boolean NOT NULL,
    CONSTRAINT "PK_Fornecedores" PRIMARY KEY ("Id")
);

CREATE TABLE "Enderecos" (
    "Id" uuid NOT NULL,
    "ProviderId" uuid NOT NULL,
    "Street" varchar(100) NOT NULL,
    "Number" varchar(10) NOT NULL,
    "Complement" varchar(100) NOT NULL,
    "Neighborhood" varchar(50) NOT NULL,
    "City" varchar(50) NOT NULL,
    "State" varchar(2) NOT NULL,
    "ZipCode" varchar(8) NOT NULL,
    CONSTRAINT "PK_Enderecos" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Enderecos_Fornecedores_ProviderId" FOREIGN KEY ("ProviderId") REFERENCES "Fornecedores" ("Id")
);

CREATE TABLE "Produtos" (
    "Id" uuid NOT NULL,
    "ProviderId" uuid NOT NULL,
    "Name" varchar(100) NOT NULL,
    "Description" varchar(1000) NOT NULL,
    "Image" varchar(200) NOT NULL,
    "Value" numeric NOT NULL,
    "DateRegistration" timestamp with time zone NOT NULL,
    "Active" boolean NOT NULL,
    CONSTRAINT "PK_Produtos" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Produtos_Fornecedores_ProviderId" FOREIGN KEY ("ProviderId") REFERENCES "Fornecedores" ("Id")
);

CREATE UNIQUE INDEX "IX_Enderecos_ProviderId" ON "Enderecos" ("ProviderId");

CREATE INDEX "IX_Produtos_ProviderId" ON "Produtos" ("ProviderId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221116004446_Initial', '6.0.11');

COMMIT;


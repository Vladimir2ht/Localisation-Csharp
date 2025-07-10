CREATE TABLE "LocalizationKeys" (
    "Id" SERIAL PRIMARY KEY,
    "Key" TEXT NOT NULL
);

CREATE TABLE "Languages" (
    "Id" SERIAL PRIMARY KEY,
    "Code" TEXT NOT NULL,
    "Name" TEXT NOT NULL
);

CREATE TABLE "Translations" (
    "Id" SERIAL PRIMARY KEY,
    "LocalizationKeyId" INTEGER NOT NULL,
    "LanguageId" INTEGER NOT NULL,
    "Value" TEXT NOT NULL,
    FOREIGN KEY ("LocalizationKeyId") REFERENCES "LocalizationKeys"("Id") ON DELETE CASCADE,
    FOREIGN KEY ("LanguageId") REFERENCES "Languages"("Id") ON DELETE CASCADE
);

INSERT INTO "LocalizationKeys" ("Key") VALUES
    ('HELLO'),
    ('GOODBYE');

INSERT INTO "Languages" ("Code", "Name") VALUES
    ('en', 'English'),
    ('ru', 'Russian');
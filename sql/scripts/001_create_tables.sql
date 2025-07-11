CREATE TABLE "LocalizationKeys" (
    "Key" TEXT PRIMARY KEY
);

CREATE TABLE "Languages" (
    "Code" TEXT PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "InUse" BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE "Translations" (
    "LocalizationKey" TEXT NOT NULL,
    "Language" TEXT NOT NULL,
    "Value" TEXT NOT NULL,
    PRIMARY KEY ("LocalizationKey", "Language"),
    FOREIGN KEY ("LocalizationKey") REFERENCES "LocalizationKeys"("Key") ON DELETE CASCADE,
    FOREIGN KEY ("Language") REFERENCES "Languages"("Code") ON DELETE CASCADE
);

INSERT INTO "LocalizationKeys" ("Key") VALUES
    ('HELLO'),
    ('GOODBYE');

INSERT INTO "Languages" ("Code", "Name") VALUES
    ('en', 'English'),
    ('tr', 'Turkish'),
    ('ru', 'Russian');
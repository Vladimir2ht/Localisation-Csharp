-- CREATE UNIQUE INDEX IX_LocalizationKeys_Key ON "LocalizationKeys"("Key");
CREATE INDEX IX_LocalizationKeys_Key_Hash ON "LocalizationKeys" USING HASH ("Key");

CREATE UNIQUE INDEX IX_Languages_Code ON "Languages" USING HASH ("Code");

CREATE UNIQUE INDEX IX_Translations_LocalizationKey_Language
    ON "Translations"("Language", "LocalizationKey");
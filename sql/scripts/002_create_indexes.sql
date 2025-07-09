CREATE UNIQUE INDEX IX_LocalizationKeys_Key ON "LocalizationKeys"(Key);

CREATE UNIQUE INDEX IX_Languages_Code ON "Languages"(Code);

CREATE UNIQUE INDEX IX_Translations_LocalizationKeyId_LanguageId
    ON "Translations"("LocalizationKeyId", "LanguageId");
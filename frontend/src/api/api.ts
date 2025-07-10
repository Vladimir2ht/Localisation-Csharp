import axios from "axios";

export interface TranslationRow {
  key: string;
  translations: Record<string, string>;
}

export async function getTranslations() {
  const response = await axios.get<TranslationRow[]>("http://localhost:2000/api/TranslationsTable");
  return response.data;
}

export async function getLanguages() {
  const response = await axios.get<string[]>("http://localhost:2000/api/Languages");
  return response.data;
}

export async function addLanguage(language: string) {
  const response = await axios.post("http://localhost:2000/api/Languages", { name: language });
  return response.data;
}

export async function addTranslationKey(key: string) {
  const response = await axios.post("http://localhost:2000/api/LocalizationKeys", { key });
  return response.data;
}
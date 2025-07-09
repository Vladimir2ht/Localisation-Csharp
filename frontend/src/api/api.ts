// Получение переводов с сервера
import axios from "axios";

export interface TranslationRow {
  key: string;
  translations: Record<string, string>; // Исправлено: значения - строки
}

export async function getTranslations() {
  // Замените URL на ваш реальный эндпоинт
  const response = await axios.get<TranslationRow[]>("http://localhost:2000/api/TranslationsTable");
  return response.data;
}
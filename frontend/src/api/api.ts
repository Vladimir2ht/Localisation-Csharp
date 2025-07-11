import axios from "axios";

export interface Language {
	code: string;
	name: string;
	inUse: boolean;
}

export interface TranslationRow {
	key: string;
	translations: Record<string, string>;
}

export async function getTranslations() {
	const response = await axios.get<TranslationRow[]>("http://localhost:2000/TranslationsTable");
	return response.data;
}

export async function getLanguages(): Promise<Language[]> {
	const response = await axios.get<Language[]>("http://localhost:2000/Languages");
	return response.data;
}

export async function addLanguage(codes: string[]) {
	const response = await axios.patch("http://localhost:2000/Languages/Enable", codes);
	return response.data;
}

export async function addTranslationKey(key: string) {
	const response = await axios.put("http://localhost:2000/LocalizationKeys", { key });
	return response.data;
}

export async function updateTranslation(key: string, langCode: string, value: string) {
	const response = await axios.put("http://localhost:2000/Translations", { key, langCode, value });
	return response;
}

export async function deleteTranslationKey(key: string) {
	const response = await axios.delete(`http://localhost:2000/LocalizationKeys/${encodeURIComponent(key)}`);
	return response.data;
}
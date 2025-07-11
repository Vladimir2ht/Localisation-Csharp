"use client";

import { useState, useEffect } from "react";
import { Header } from "../components/Header";
import { TranslationsTable } from "../components/TranslationsTable";
import { Pagination } from "../components/Pagination";
import { getTranslations, getLanguages, addLanguage, addTranslationKey, TranslationRow, Language, updateTranslation } from "../api/api";
import AddLanguageModal from "../components/AddLanguageModal";

/* const INITIAL_DATA = [
	{
		key: "start_game",
		translations: {
			English: "Start Game",
			Spanish: "Empezar Juego",
			French: "Commencer le jeu",
		},
	},
	{
		key: "pause_game",
		translations: {
			English: "Pause Game",
			Spanish: "Pausar Juego",
			French: "Mettre le jeu en pause",
		},
	},
	{
		key: "resume_game",
		translations: {
			English: "Resume Game",
			Spanish: "Reanudar Juego",
			French: "Reprendre le jeu",
		},
	},
	{
		key: "game_over",
		translations: {
			English: "Game Over",
			Spanish: "Fin del Juego",
			French: "Fin de la partie",
		},
	},
	{
		key: "score",
		translations: {
			English: "Score",
			Spanish: "Puntuación",
			French: "Score",
		},
	},
	{
		key: "level",
		translations: {
			English: "Level",
			Spanish: "Nivel",
			French: "Niveau",
		},
	},
	{
		key: "time",
		translations: {
			English: "Time",
			Spanish: "Tiempo",
			French: "Temps",
		},
	},
	{
		key: "settings",
		translations: {
			English: "Settings",
			Spanish: "Ajustes",
			French: "Paramètres",
		},
	},
	{
		key: "sound",
		translations: {
			English: "Sound",
			Spanish: "Sonido",
			French: "Son",
		},
	},
	{
		key: "music",
		translations: {
			English: "Music",
			Spanish: "Música",
			French: "Musique",
		},
	},
]; */

export default function Home() {
	const [data, setData] = useState<TranslationRow[]>([]);
	const [languages, setLanguages] = useState<Language[]>([]);
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState<string | null>(null);
	const [currentPage, setCurrentPage] = useState(1);
	const [showAddLang, setShowAddLang] = useState(false);
	const pageSize = 5;

	async function fetchData() {
		try {
			const [langs, translations] = await Promise.all([
				getLanguages(),
				getTranslations(),
			]);
			setLanguages(langs);
			setData(translations);
			setLoading(false);
		} catch {
			setError("Ошибка загрузки данных");
			setLoading(false);
		}
	}

	useEffect(() => {
		fetchData();
	}, []);

	const enabledLanguages = languages.filter(l => l.inUse);

	async function handleEdit(key: string, lang: string, value: string) {
		try {
			await updateTranslation(key, lang, value);
			setData(prev =>
				prev.map(item =>
					item.key === key
						? {
							...item,
							translations: { ...item.translations, [lang]: value },
						}
						: item
				)
			);
		} catch {
			// Ошибка сохранения, не обновляем UI
		}
	}

	async function handleAddLanguage(codes: string[]) {
		await addLanguage(codes);
		fetchData();
		setShowAddLang(false);
	}

	async function handleAddKey(key: string) {
		await addTranslationKey(key);
		fetchData();
	}

	async function handleDeleteKey(key: string) {
		setData(prev => prev.filter(item => item.key !== key));
		await fetchData();
	}

	const totalPages = Math.ceil(data.length / pageSize);
	const pagedData = data.slice((currentPage - 1) * pageSize, currentPage * pageSize);

	if (loading) return <div className="p-10 text-center">Загрузка...</div>;
	if (error) return <div className="p-10 text-center text-red-500">{error}</div>;

	return (
		<div className="relative flex min-h-screen flex-col bg-white overflow-x-hidden" style={{ fontFamily: 'Inter, "Noto Sans", sans-serif' }}>
			<div className="layout-container flex h-full grow flex-col">
				<Header
					onAddLanguage={() => setShowAddLang(true)}
					onAddKey={handleAddKey}
					onDeleteKey={handleDeleteKey}
					keys={data.map(d => d.key)}
				/>
				<main className="px-40 flex flex-1 justify-center py-5">
					<div className="layout-content-container flex flex-col max-w-[960px] flex-1">
						<div className="flex flex-wrap justify-between gap-3 p-4">
							<p className="text-[#111418] tracking-light text-[32px] font-bold leading-tight min-w-72">
								Translations
							</p>
						</div>
						<TranslationsTable languages={enabledLanguages
						.map(l => ({ name: l.name, code: l.code }))} data={pagedData} onEdit={handleEdit} />
					</div>
				</main>
				<Pagination currentPage={currentPage} totalPages={totalPages} onPageChange={setCurrentPage} />
			</div>
			{showAddLang && (
				<AddLanguageModal
					open={showAddLang}
					languages={languages}
					onConfirm={handleAddLanguage}
					onCancel={() => setShowAddLang(false)}
				/>
			)}
		</div>
	);
}
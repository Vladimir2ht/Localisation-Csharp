import React from "react";
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogFooter } from "@/components/ui/dialog";
import { Select, SelectTrigger, SelectValue, SelectContent, SelectItem } from "@/components/ui/select";
import { Button } from "@/components/ui/button";
import { Language } from "../api/api";

interface AddLanguageModalProps {
	open: boolean;
	languages: Language[];
	onConfirm: (ids: number[]) => void;
	onCancel: () => void;
}

const AddLanguageModal: React.FC<AddLanguageModalProps> = ({ open, languages, onConfirm, onCancel }) => {
	const [selectedIds, setSelectedIds] = React.useState<number[]>([]);
	const [currentSelect, setCurrentSelect] = React.useState<number | null>(null);

	const availableLanguages = languages.filter(l => !selectedIds.includes(l.id));

	function handleAddSelect() {
		if (currentSelect && !selectedIds.includes(currentSelect)) {
			setSelectedIds([...selectedIds, currentSelect]);
			setCurrentSelect(null);
		}
	}

	function handleRemove(id: number) {
		setSelectedIds(selectedIds.filter(i => i !== id));
	}

	function handleConfirm() {
		onConfirm(selectedIds);
		setSelectedIds([]);
	}

	React.useEffect(() => {
		if (!open) {
			setSelectedIds([]);
			setCurrentSelect(null);
		}
	}, [open]);

	return (
		<Dialog open={open} onOpenChange={v => { if (!v) onCancel(); }}>
			<DialogContent>
				<DialogHeader>
					<DialogTitle>Добавить языки</DialogTitle>
				</DialogHeader>
				<div className="space-y-2">
					{selectedIds.map(id => {
						const lang = languages.find(l => l.id === id);
						return lang ? (
							<div key={id} className="flex items-center gap-2">
								<span>{lang.name} ({lang.code})</span>
								<Button variant="outline" size="sm" onClick={() => handleRemove(id)}>Удалить</Button>
							</div>
						) : null;
					})}
					<div className="flex gap-2 items-center">
						<Select value={currentSelect ? String(currentSelect) : undefined} onValueChange={val => setCurrentSelect(Number(val))}>
							<SelectTrigger className="w-[200px]">
								<SelectValue placeholder="Выберите язык" />
							</SelectTrigger>
							<SelectContent>
								{availableLanguages.map(lang => (
									<SelectItem key={lang.id} value={String(lang.id)}>
										{lang.name} ({lang.code})
									</SelectItem>
								))}
							</SelectContent>
						</Select>
						<Button onClick={handleAddSelect} disabled={!currentSelect}>Добавить</Button>
					</div>
				</div>
				<DialogFooter>
					<Button onClick={handleConfirm} disabled={selectedIds.length === 0}>Подтвердить</Button>
					<Button variant="outline" onClick={onCancel}>Отмена</Button>
				</DialogFooter>
			</DialogContent>
		</Dialog>
	);
};

export default AddLanguageModal;
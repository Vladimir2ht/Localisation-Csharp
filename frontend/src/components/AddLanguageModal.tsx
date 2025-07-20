import React from "react";
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogFooter } from "@/components/ui/dialog";
import { Select, SelectTrigger, SelectValue, SelectContent, SelectItem } from "@/components/ui/select";
import { Button } from "@/components/ui/button";
import { Language } from "../api/api";

interface AddLanguageModalProps {
	open: boolean;
	languages: Language[];
	onConfirm: (codes: string[]) => void;
	onCancel: () => void;
}

const AddLanguageModal: React.FC<AddLanguageModalProps> = ({ open, languages, onConfirm, onCancel }) => {
	const [selectedCodes, setSelectedIds] = React.useState<string[]>([]);
	const [currentSelect, setCurrentSelect] = React.useState<string | null>(null);

	const availableLanguages = languages.filter(l => !selectedCodes.includes(l.code));

	function handleAddSelect() {
		if (currentSelect && !selectedCodes.includes(currentSelect)) {
			setSelectedIds([...selectedCodes, currentSelect]);
			setCurrentSelect(null);
		}
	}

	function handleRemove(code: string) {
		setSelectedIds(selectedCodes.filter(i => i !== code));
	}

	function handleConfirm() {
		onConfirm(selectedCodes);
		setSelectedIds([]);
	}

	React.useEffect(() => {
		if (!open) {
			setSelectedIds([]);
			setCurrentSelect(null);
		}
	}, [open]);

	return <Dialog open={open} onOpenChange={v => { if (!v) onCancel(); }}>
		<DialogContent>
			<DialogHeader>
				<DialogTitle>Добавить языки</DialogTitle>
			</DialogHeader>
			<div className="space-y-2">
				{selectedCodes.map(code => {
					const lang = languages.find(l => l.code === code);
					return lang ? (
						<div key={code} className="flex items-center gap-2">
							<span>{lang.name} ({lang.code})</span>
							<Button variant="outline" size="sm" onClick={() => handleRemove(code)}>Удалить</Button>
						</div>
					) : null;
				})}
				<div className="flex gap-2 items-center">
					<Select value={currentSelect ? String(currentSelect) : undefined} onValueChange={val => setCurrentSelect(val)}>
						<SelectTrigger className="w-[200px]">
							<SelectValue placeholder="Выберите язык" />
						</SelectTrigger>
						<SelectContent>
							{availableLanguages.map(lang => (
								<SelectItem key={lang.code} value={String(lang.code)}>
									{lang.name} ({lang.code})
								</SelectItem>
							))}
						</SelectContent>
					</Select>
					<Button onClick={handleAddSelect} disabled={!currentSelect}>Добавить</Button>
				</div>
			</div>
			<DialogFooter>
				<Button onClick={handleConfirm} disabled={selectedCodes.length === 0}>Подтвердить</Button>
				<Button variant="outline" onClick={onCancel}>Отмена</Button>
			</DialogFooter>
		</DialogContent>
	</Dialog>;
};

export default AddLanguageModal;
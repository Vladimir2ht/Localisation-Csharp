"use client";

import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Select, SelectTrigger, SelectValue, SelectContent, SelectItem } from "@/components/ui/select";
import { useState } from "react";
import { deleteTranslationKey } from "@/api/api";

interface HeaderProps {
  onAddLanguage: () => void;
  onAddKey: (key: string) => void;
  onDeleteKey?: (key: string) => void;
  keys: string[];
}

export function Header({ onAddLanguage, onAddKey, onDeleteKey, keys }: HeaderProps) {
  const [showKeyInput, setShowKeyInput] = useState(false);
  const [keyValue, setKeyValue] = useState("");
  const [showDeleteInput, setShowDeleteInput] = useState(false);
  const [selectedDeleteKey, setSelectedDeleteKey] = useState<string>("");
  const [deleting, setDeleting] = useState(false);
  const [deleteError, setDeleteError] = useState<string | null>(null);

  function handleAddKey() {
    if (keyValue.trim()) {
      onAddKey(keyValue.trim());
      setKeyValue("");
      setShowKeyInput(false);
    }
  }

  async function handleDeleteKey() {
    if (!selectedDeleteKey) return;
    setDeleting(true);
    setDeleteError(null);
    try {
      await deleteTranslationKey(selectedDeleteKey);
      if (onDeleteKey) onDeleteKey(selectedDeleteKey);
      setSelectedDeleteKey("");
      setShowDeleteInput(false);
    } catch (e) {
      // @ts-expect-error: axios error type may not have response property
      setDeleteError((e?.response?.data?.message as string) || "Ошибка удаления");
    } finally {
      setDeleting(false);
    }
  }

  return <header className="flex items-center justify-between whitespace-nowrap border-b border-b-[#f0f2f5] px-10 py-3">
    <div className="flex items-center gap-4 text-[#111418]">
      <div className="w-10 h-10">
        <svg
          viewBox="0 0 48 48"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
          className="w-full h-full text-current"
        >
          <path
            d="M24 45.8096C19.6865 45.8096 15.4698 44.5305 11.8832 42.134C8.29667 39.7376 5.50128 36.3314 3.85056 32.3462C2.19985 28.361 1.76794 23.9758 2.60947 19.7452C3.451 15.5145 5.52816 11.6284 8.57829 8.5783C11.6284 5.52817 15.5145 3.45101 19.7452 2.60948C23.9758 1.76795 28.361 2.19986 32.3462 3.85057C36.3314 5.50129 39.7376 8.29668 42.134 11.8833C44.5305 15.4698 45.8096 19.6865 45.8096 24L24 24L24 45.8096Z"
            fill="currentColor"
          />
        </svg>
      </div>
      <h2 className="text-[#111418] text-lg font-bold leading-tight tracking-[-0.015em]">
        Game Localization Manager
      </h2>
    </div>
    <div className="flex flex-1 justify-end gap-8">
      <Label className="flex flex-col min-w-[160px] max-w-[256px] h-10">
        <div className="flex w-full flex-1 items-stretch rounded-lg h-full">
          <div className="text-[#60758a] flex items-center justify-center pl-4 rounded-l-lg border-r-0 bg-[#f0f2f5]">
          </div>
          {/* <Input
              placeholder="Search"
              className="flex w-full min-w-0 flex-1 resize-none overflow-hidden rounded-l-none border-l-0 pl-2 text-[#111418] text-base font-normal leading-normal placeholder:text-[#60758a] h-full bg-[#f0f2f5] focus:outline-none focus:ring-0 border-none"
              type="search"
              name="search"
              aria-label="Search"
            /> */}
        </div>
      </Label>
      <div className="flex gap-2">
        {showKeyInput ? (
          <div className="flex gap-2">
            <Input
              autoFocus
              value={keyValue}
              onChange={e => setKeyValue(e.target.value)}
              onKeyDown={e => { if (e.key === "Enter") handleAddKey(); if (e.key === "Escape") setShowKeyInput(false); }}
              placeholder="New key"
              className="h-10"
            />
            <Button onClick={handleAddKey} className="h-10 bg-[#0c7ff2] text-white">OK</Button>
            <Button variant="outline" onClick={() => setShowKeyInput(false)} className="h-10">Cancel</Button>
          </div>
        ) : (
          <Button className="min-w-[84px] max-w-[480px] h-10 bg-[#0c7ff2] text-white text-sm font-bold tracking-[0.015em]" onClick={() => setShowKeyInput(true)}>
            Add Key
          </Button>
        )}
        {showDeleteInput ? (
          <div className="flex gap-2 items-center">
            <Select value={selectedDeleteKey} onValueChange={setSelectedDeleteKey}>
              <SelectTrigger className="w-[200px]">
                <SelectValue placeholder="Выберите ключ для удаления" />
              </SelectTrigger>
              <SelectContent>
                {keys.map(key => (
                  <SelectItem key={key} value={key}>{key}</SelectItem>
                ))}
              </SelectContent>
            </Select>
            <Button onClick={handleDeleteKey} className="h-10 bg-red-500 text-white" disabled={deleting || !selectedDeleteKey}>Удалить</Button>
            <Button variant="outline" onClick={() => setShowDeleteInput(false)} className="h-10" disabled={deleting}>Cancel</Button>
            {deleteError && <span className="text-red-500 ml-2">{deleteError}</span>}
          </div>
        ) : (
          <Button className="min-w-[84px] max-w-[480px] h-10 bg-red-500 text-white text-sm font-bold tracking-[0.015em]" onClick={() => setShowDeleteInput(true)}>
            Delete Key
          </Button>
        )}
        <Button
          variant="outline"
          className="min-w-[84px] max-w-[480px] h-10 text-[#111418] bg-[#f0f2f5] font-bold text-sm tracking-[0.015em]"
          onClick={onAddLanguage}
        >
          Add Language
        </Button>
      </div>
    </div>
  </header>;
}
"use client";

import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { useState } from "react";

interface HeaderProps {
  onAddLanguage: (language: string) => void;
  onAddKey: (key: string) => void;
}

export function Header({ onAddLanguage, onAddKey }: HeaderProps) {
  const [showLangInput, setShowLangInput] = useState(false);
  const [showKeyInput, setShowKeyInput] = useState(false);
  const [langValue, setLangValue] = useState("");
  const [keyValue, setKeyValue] = useState("");

  function handleAddLang() {
    if (langValue.trim()) {
      onAddLanguage(langValue.trim());
      setLangValue("");
      setShowLangInput(false);
    }
  }
  function handleAddKey() {
    if (keyValue.trim()) {
      onAddKey(keyValue.trim());
      setKeyValue("");
      setShowKeyInput(false);
    }
  }

  return (
    <header className="flex items-center justify-between whitespace-nowrap border-b border-b-[#f0f2f5] px-10 py-3">
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
          {showLangInput ? (
            <div className="flex gap-2">
              <Input
                autoFocus
                value={langValue}
                onChange={e => setLangValue(e.target.value)}
                onKeyDown={e => { if (e.key === "Enter") handleAddLang(); if (e.key === "Escape") setShowLangInput(false); }}
                placeholder="New language"
                className="h-10"
              />
              <Button onClick={handleAddLang} className="h-10 bg-[#0c7ff2] text-white">OK</Button>
              <Button variant="outline" onClick={() => setShowLangInput(false)} className="h-10">Cancel</Button>
            </div>
          ) : (
            <Button
              variant="outline"
              className="min-w-[84px] max-w-[480px] h-10 text-[#111418] bg-[#f0f2f5] font-bold text-sm tracking-[0.015em]"
              onClick={() => setShowLangInput(true)}
            >
              Add Language
            </Button>
          )}
        </div>
      </div>
    </header>
  );
}
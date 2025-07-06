"use client";

import { useState } from "react";
import { Input } from "@/components/ui/input";

interface TranslationRow {
  key: string;
  translations: Record<string, string>;
}

interface TranslationsTableProps {
  languages: string[];
  data: TranslationRow[];
  onEdit: (key: string, lang: string, value: string) => void;
}

export function TranslationsTable({ languages, data, onEdit }: TranslationsTableProps) {
  const [editing, setEditing] = useState<{ key: string; lang: string } | null>(null);
  const [editValue, setEditValue] = useState("");

  function startEditing(key: string, lang: string, currentValue: string) {
    setEditing({ key, lang });
    setEditValue(currentValue);
  }

  function saveEdit() {
    if (editing) {
      onEdit(editing.key, editing.lang, editValue);
      setEditing(null);
    }
  }

  function cancelEdit() {
    setEditing(null);
  }

  return (
    <div className="px-4 py-3 @container">
      <div className="flex overflow-hidden rounded-lg border border-[#dbe0e6] bg-white">
        <table className="flex-1 table-fixed border-collapse">
          <thead>
            <tr className="bg-white">
              <th className="w-[400px] px-4 py-3 text-left text-[#111418] text-sm font-medium leading-normal">
                Key
              </th>
              {languages.map((lang, idx) => (
                <th
                  key={lang}
                  className={`w-[400px] px-4 py-3 text-left text-[#111418] text-sm font-medium leading-normal table-column-${(idx + 1) * 120}`}
                >
                  {lang}
                </th>
              ))}
            </tr>
          </thead>
          <tbody>
            {data.map(({ key, translations }) => (
              <tr key={key} className="border-t border-t-[#dbe0e6]">
                <td className="w-[400px] h-[72px] px-4 py-2 text-[#111418] text-sm font-normal leading-normal whitespace-nowrap">
                  {key}
                </td>
                {languages.map((lang, idx) => {
                  const isEditing = editing?.key === key && editing.lang === lang;
                  const value = translations[lang] ?? "";
                  return (
                    <td
                      key={lang}
                      className={`w-[400px] h-[72px] px-4 py-2 text-[#60758a] text-sm font-normal leading-normal cursor-pointer table-column-${(idx + 1) * 120}`}
                      onClick={() => !isEditing && startEditing(key, lang, value)}
                    >
                      {isEditing ? (
                        <Input
                          autoFocus
                          value={editValue}
                          onChange={(e) => setEditValue(e.target.value)}
                          onBlur={saveEdit}
                          onKeyDown={(e) => {
                            if (e.key === "Enter") saveEdit();
                            if (e.key === "Escape") cancelEdit();
                          }}
                          className="h-8"
                        />
                      ) : (
                        value
                      )}
                    </td>
                  );
                })}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <style>
        {`
          @container(max-width:120px) {
            .table-column-120 {display:none;}
          }
          @container(max-width:240px) {
            .table-column-240 {display:none;}
          }
          @container(max-width:360px) {
            .table-column-360 {display:none;}
          }
          @container(max-width:480px) {
            .table-column-480 {display:none;}
          }
        `}
      </style>
    </div>
  );
}
# Elemente Hinzufügen

Um ein Element anzulegen, müssen Sie das Blatt `GAEB` auswählen und in Spalte `A - Typ` einen der folgenden Werte eintragen:

* `Position` (oder `Item`) für Positionen
* `Gruppe` (oder `Group`) für Gruppen
* `Hinweistext` (oder `Note Text`) für Textergänzungen
* `Ausführungsbeschreibung` (oder `Execution Description`) für Ausführungsbeschreibungen

Bitte achten Sie darauf, keine anderen Werte einzutragen, da diese Zeilen sonst ohne Fehlermeldung von Web**GAEB** ignoriert werden.

## Unterstütze Eigenschaften

Abhängig vom Elementtyp können Sie folgende Eigenschaften vergeben:

* `Position` unterstützt `Ordnungszahl`, `Kurztext`, `Langtext`, `Menge`, `Einheit`, `Einheitspreis`, `Nachlass` und `MwSt.` (wenn nicht in der Position angegeben wird der projektweite Mehrwertsteuersatz benutzt).
* `Gruppe` unterstützt `Ordnungszahl`, `Kurztext` und `Nachlass`.
* `Hinweistext` unterstützt `Kurztext` und `Langtext`.
* `Ausführungsbeschreibung` unterstützt lediglich `Kurztext`. Es ist nicht empfohlen, diese über den Excelimport zu erstellen.

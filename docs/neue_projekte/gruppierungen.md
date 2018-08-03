# Gruppierungen Erstellen

Möchten Sie Ihr Projekt in Gruppen unterteilen bzw. eine Gliederung erstellen, können Sie mit dem Element `Gruppe` arbeiten. Sobald das Element `Gruppe` beim Import erkannt wird, werden alle nachfolgenden Elemente, die mit der gleichen Ordnungszahl wie die Gruppe beginnen, darunter eingeordnet.

Folgende Beispieleingabe in Excel:

| Typ      | Ordnungszahl | Kurztext            |
|----------|--------------|---------------------|
| Gruppe   | 01           | Rohbauarbeiten      |
| Gruppe   | 01.01        | Erdgeschoss         |
| Position | 01.01.01     | Wände               |
| Position | 01.01.02     | Decken              |
| Gruppe   | 02           | Aussenanlagen       |
| Position | 02.01        | Baumarbeiten        |
| Gruppe   | 03           | Stundenlohnarbeiten |
| Position | 03.01        | Stundenlohn Geselle |

Erzeugt folgende Projektstruktur:

    - 01. Rohbauarbeiten
      - 01.01. Erdgeschoss
        - 01.01.01 Wände
        - 01.01.02 Decken
    - 02. Aussenanlagen
      - 02.01. Baumarbeiten
    - 03. Stundenlohnarbeiten
      - 03.01 Stundenlohn Geselle

Sie erkennen an diesem Beispiel, dass `Gruppe 02.` automatisch als neue Gruppe in der obersten Ebene definiert wird, da keine der vorangehenden Gruppen mit der selben Ordnungszahl beginnen. Das bedeutet, Sie müssen Gruppen nicht manuell schließen. Wenn Sie die Datei erneut mit Web**GAEB** konvertieren, werden die Gruppenendfelder automatisch hinzugefügt.  
Um kompatibel mit dem GAEB Standard zu sein, sollten Sie darauf achten, keine Gruppen und Positionen in der selben Gliederungsstufe eines Projektes zu vermischen.

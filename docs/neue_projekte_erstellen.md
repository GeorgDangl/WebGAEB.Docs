# Neue Projekte Erstellen

Beim Öffnen der Projektvorlage finden Sie ein Arbeitsblatt namens `GAEB`. In diesem Blatt können Sie neue Elemente erstellen.

## Elemente Hinzufügen

Um ein Element anzulegen, müssen Sie in Spalte `A - Typ` einen der folgenden Werte eintragen:

* `Position` (oder `Item`) für Positionen
* `Gruppe` (oder `Group`) für Gruppen
* `Hinweistext` (oder `Note Text`) für Textergänzungen
* `Ausführungsbeschreibung` (oder `Execution Description`) für Ausführungsbeschreibungen

### Gruppierungen Erstellen

Möchten Sie Ihr Projekt in Gruppen unterteilen bzw. eine Gliederung erstellen, können Sie mit dem Element `Gruppe` arbeiten. Sobald das Element `Gruppe` beim Import erkannt wird, werden alle nachfolgenden Element, die mit der gleichen Ordnungszahl wie die Gruppe beginnen, darunter eingeordnet.

Folgende Beispieleingabe in Excel:

| Typ      | Ordnungszahl | Kurztext            |
|----------|--------------|---------------------|
| Gruppe   | 01           | Rohbauarbeiten      |
| Gruppe   | 01.01        | Erdgeschoss         |
| Position | 01.01.01     | Wände               |
| Position | 01.01.02     | Decken              |
| Gruppe   | 02           | Aussenanlagen       |
| Position | 02.01        | Baumarbeiten        |
| Position | 03           | Stundenlohn Geselle |

Erzeugt folgende Projektstruktur:

    - 01. Rohbauarbeiten
      - 01.01. Erdgeschoss
        - 01.01.01 Wände
        - 01.01.02 Decken
    - 02. Aussenanlagen
      - 02.01. Baumarbeiten
    - 03. Stundenlohn Geselle

Sie erkennen an diesem Beispiel, dass `Position 03.` nicht mehr unter eine Gruppe eingeordnet wird, da keine der vorangehenden Gruppen mit der selben Ordnungszahl beginnen.

## Hinweise zum Arbeiten mit Ordnungszahlen

Im Projektsystem werden Ordnungszahlebenen durch einen `.` Punkt getrennt. Zum Beispiel besteht die Ordnungszahl `01.03` aus der ersten Ebene `01` und der zweiten Ebene `03`.


### Erlaubte Zeichen

Obwohl das Projektsystem kein Einschränkungen aufweist, gibt es leider bestimmte Vorgaben im GAEB XML Standard, nach denen sich alle Ordnungszahlen richten müssen. Erlaubt sind nur Groß- und Kleinbuchstaben, Zahlen sowie der `_` Unterstrich. Alle nicht-gültigen Zeichen werden mit `_` ersetzt.

### Gruppierungen

Wenn Sie Gruppen in Ihrer Projektstruktur einsetzen möchten, beachten Sie bitte die folgenden Hinweise:

* Gruppen der obersten Ebene sollten keine Punkte in den Ordnungszahlen haben, Gruppen der zweiten Ebene einen Punkt usw. Das bedeutet, dass Ihre Gruppen der ersten Ebene z.B. die Ordnungszahlen `01`, `02` und `03` bekommen. `01.01` sollten Gruppen der zweiten Ebene, unter der ersten, vorbehalten werden.

## Sonstiges

Möchten Sie nach GAEB XML exportieren, beachten Sie bitte, dass in der selben Ebene nur entweder Gruppen oder Positionen vorkommen dürfen, jedoch nicht beide zusammen. Das heißt, folgende Projektstruktur ist ungültig:

    - 01. Gruppe
      - 01.01 Position A
      - 01.02 Position B
      - 01.03 Gruppe, untergeordnet

Fügen Sie stattdessen eine weitere Gruppe hinzu, um die Positionen `A` und `B` zu gruppieren:

    - 01. Gruppe
      - 01.01 Gruppe, untergeordnet
        - 01.01.01 Position A
        - 01.01.02 Position B
      - 01.03 Gruppe, untergeordnet

Obwohl die meisten AVA-Programme solche Dateien lesen können, zeigen Sie in der Regel Fehler beim Import an. Der Grund dahinter ist, dass eine solche Anordnung nicht im GAEB XML Standard vorgesehen ist.

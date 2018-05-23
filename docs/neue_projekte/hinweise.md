# Allgemeine Hinweise

Obwohl das Projektsystem sehr viele Freiheiten bietet, müssen Sie ein paar Punkte beachten, wenn Sie vor haben, die Daten
später im GAEB Format auszutauschen.

## Hinweise zum Arbeiten mit Ordnungszahlen

Im Projektsystem werden Ordnungszahlebenen durch einen `.` Punkt getrennt. Zum Beispiel besteht die Ordnungszahl `01.03` aus der ersten Ebene `01` und der zweiten Ebene `03`.

### Erlaubte Zeichen

Obwohl das Projektsystem kein Einschränkungen aufweist, gibt es leider bestimmte Vorgaben im GAEB XML Standard, nach denen sich alle Ordnungszahlen richten müssen. Erlaubt sind nur Groß- und Kleinbuchstaben, Zahlen sowie der `_` Unterstrich. Alle nicht-gültigen Zeichen werden mit `_` ersetzt.

### Gruppierungen

Wenn Sie Gruppen in Ihrer Projektstruktur einsetzen möchten, beachten Sie bitte die folgenden Hinweise:

* Gruppen der obersten Ebene sollten keine Punkte in den Ordnungszahlen haben, Gruppen der zweiten Ebene einen Punkt usw. Das bedeutet, dass Ihre Gruppen der ersten Ebene z.B. die Ordnungszahlen `01`, `02` und `03` bekommen. `01.01` sollten Gruppen der zweiten Ebene, unter der ersten, vorbehalten werden.

### Limitierungen

Um volle GAEB Kompatibilität zu erhalten, müssen Sie darauf achten, Ihre Projektstruktur nicht zu tief anzulegen und die Gesamtlänge der Ordnungszahlen unter einem Bestimmten Wert zu halten.

Grundsätzlich gilt, bei **GAEB 90** darf die Gesamtlänge nicht mehr als **9** Zeichen betragen, in **GAEB 2000** und **GAEB XML** liegt das Limit bei **14** Zeichen. Punkte zur Trennung zwischen Ebenen werden hierbei nicht mitgezählt. Das bedeutet, die Ordnungszahl `01.01.005.02` hätte eine rechnerische Länge von **9** Zeichen und passt somit gerade noch in das GAEB 90 Schema.

Intern versucht der Konverter Korrekturen durchzuführen, falls ein Limit erreicht wird. Zum Beispiel können führende Nullen weggelassen werden, um Ordnungszahlen zu kürzen. Das funktioniert jedoch nicht in jedem Fall, und der Konverter wird keine Änderungen vornehmen, die z.B. die Datenreihenfolge verändern würde.

Ebenfalls sollten Sie nicht mehr als **vier Ebenen** (für **GAEB 90**) bzw. **sechs Ebenen** (für **GAEB 2000** und **GAEB XML**) nutzen.

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

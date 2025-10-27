[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/CXZWIhbz)
# Übung 3
## Intro
In dieser Woche haben wir kennengelernt, welche biologische Bedeutung die Sekundärstrukturen von RNA-Sequenzen haben.
Daher werden wir in der Übung diese Woche ein Tool zur Vorhersage von einfachen RNA-Sekundärsturkturen bauen. Um
die Aufgabe nicht unnötig komplex Gestalten, werden wir ein paar Annahmen treffen, die biologisch nicht sinnvoll sind
aber die Aufgabe stark vereinfachen.

## Annahmen

1. Wir gehen davon aus, dass eine Sequenz von einer Base zur andern reverskomplementär zurücklaufen kann. Die Mindestgröße von Loops ist also 0. Folgendes wäre also Valide:

```
Sequenz:               GGATGCGCAT
Result Vienna Format:  ..(((())))
G-G-A-T-G-C-|
    | | | | |
    T-A-C-G-|
```
2. Wir sagen ausschließlich die längsten Subalignments vorraus.
```
Sequenz: CCGGATGCGCAT
Result:  ....(((())))
C-C-G-G-A-T-G-C-|
        | | | | |
        T-A-C-G-|
```
`CCGG` Ignorieren wir hier also.

## Aufgaben zum Programmieren
**is_comp**

Definieren Sie im Skript `rnaSeqPred.py` die Funktion `is_comp(base_i, base_j)`. Diese Funktion soll `True` zurück geben,
wenn `base_i` komplementär zu `base_j`ist. Andernfalls soll `False` zurückgeben werden. Die Funktion soll unabhängig von der
Groß- und Kleinschreibung von `base_i` und `base_j` funktionieren. Zudem soll die Funktion sowohl für das RNA Alphabet(A,U,C,G) als auch für das DNA-Alphabet(A,T,C,G) funktionieren. Wenn base_i und base_j keine einzellnen Characters sind aus dem Alphabet [A,T,C,G,U,a,c,t,g,u], soll ein ValueError geworfen werden.

**zero_matrix**

Defenieren Sie eine Funktion `zero_matrix(size)` die eine `n x n` Matrix zurückgibt, bei der jeder Wert 0 ist.
Achten Sie dabei darauf, dass die Matrixzeilen und unabhängig voneinander sind.

***Beispiel***
```
print(zero_matrix(3))
# [[0,0,0],[0,0,0],[0,0,0]]
```
**dynmatrix**

Definieren Sie nun die Funktion `dnymatrix(rna_sequence)`. Diese Funktion soll ebenfalls eine Matrix zurückgeben,
mit den Dimensionen `len(rna_sequence) x len(rna_sequence)`. Die Hauptdiagonale der Matrix und alle Werte darunter sollen
null sein. Also wenn `j<=i` dann ist der Eintrag 0. Werfen Sie einen `AssertionError`, wenn die Länge der rna_sequence kleiner als 2 ist.

Des Weiteren wird die Oberedreiecksmatrix wie folgt berechnet:
Der Wert der `Matrix[i,j]` ist das Maximum von:
 - `Matrix[i+1,j]` (unten)
 - `Matrix[i,j-1]` (links)
 - wenn `rna_sequence[i]` reverskomplementär zu `rna_sequence[j]` --> `Matrix[i+1,j-1] + 1` (unten-links)
 - wenn `rna_sequence[i]` **nicht** komplementär zu `rna_sequence[j]` --> `Matrix[i+1,j-1] + 0` (unten-links)

***Achtung:
Beachten Sie die Reihenfolge in der Sie die Matrixbefüllen, da die Felder voneinander abhängig sind!!!***


# Aufgaben zum  hier ausfüllen
## Aufgabe 1 
*1 Punkt*

Nutzen Sie Ihren Code, um die Liste der beiden Vienna Strings für die Sequenz `"AAUUCCGG"` zu generieren.

```
Ergebnis Code: 
```

## Aufgabe 2 
*3 Punkte*
Überlegen Sie sich DAS RNA Alignment im Vienna Format (Format das bei traceback rauskommt) für die Sequenz `AAUUCCGG`, dass **alle** Basen in der Sequenz mit einer anderen verpaart!.

**Vienna Format**

- Ungepaarte Basen bekommen ein `'.'` zugewiesen. 
- Eine Base die in Sequenzrichtung alliniert, bekommt Sie eine öffnende Klammer `(` zugewiesen.
- Eine reverskomplementär allinierte Base bekommt eine schließende Klammer zugwiesen `')'` und gehört immer zu einer öffnenden Klammer!

```
Mein Ergebnis: 
```



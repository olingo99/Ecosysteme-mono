# Ecosystem

![Rendu Visuel](/renduVisuel.png)

## UML Diagrams

### Class diagram
![class diagram](/diagramme_png/Classs.png)
### Sequence diagram
#### Diagramme de Sequence de GetPlay lorsque l'etre vivant est une plante
![Sequence diagram](/diagramme_png/SeqDiaPlante.jpg)

#### to modify , sequence diagram of drawing animal
![Sequence diagram](/diagramme_png/Sequence.png)

## SOLID principles justification

### Single-responsibility principle
> "There should never be more than one reason for a class to change."

> "A class should have only one reason to change." — Robert C. Martin

Le principe de responsabilité unique est respecté dans la classe *Factory* . Cette classe possède un et un seul objectif, celui de creer de nouvelles entités sur le plateau.

### Open–closed principle

> Objects or entities should be open for extension , but closed for modification.

La classe abstraite *EtreVivant* respecte bien ce principe, il lui est en effet facile d'ajouter un type d'etre vivant en créant une nouvelle classe qui en herite (open). L'ajout de cette nouvele classe n'implique pas de modification de code dans celles déjà existantes et n'influe pas non plus leur fonctionnement (close). 


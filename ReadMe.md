# Ecosystem

![Rendu Visuel](/renduVisuel.png)

## UML Diagrams

### Class diagram
![class diagram](/diagramme_png/Classs.png)
### Sequence diagram
![Sequence diagram](/diagramme_png/Sequence.png)

## SOLID principles justification

### Single-responsibility principle
> "There should never be more than one reason for a class to change."

> "A class should have only one reason to change." — Robert C. Martin

Le principe de responsabilité unique est respecté dans la classe *Factory* . Cette classe possède un et un seul objectif, celui de creer de nouvelles entités sur le plateau.

### Open–closed principle

> Objects or entities should be open for extension , but closed for modification.

La classe abstraite *EtreVivant* respecte bien ce principe, il lui est en effet facile d'ajouter un type d'etre vivant en créant une nouvelle classe qui en herite (open) mais celle-ci ne procure aucun changement sur les autres classes déjà presentes. (close) 


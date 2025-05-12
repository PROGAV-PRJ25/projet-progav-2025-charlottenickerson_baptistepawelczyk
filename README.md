# Simulateur de Plantes

Ce projet est un simulateur de jardinage qui permet de cultiver différentes plantes dans un environnement interactif en console. Il simule la croissance des plantes, l'influence des conditions météorologiques et offre un système de gestion de parcelles.

## Fonctionnalités Ajoutées Récemment

### 1. Système de Croissance Progressive des Plantes

- **Cycle de Vie Réaliste** : Chaque type de plante passe par des étapes de croissance spécifiques (germination, plantule, croissance, floraison, etc.) jusqu'à maturité
- **Durées de Croissance Personnalisées** : Chaque plante a son propre rythme de croissance, certaines mûrissent rapidement (comme le radis) et d'autres plus lentement (comme l'ananas)
- **Représentation Visuelle Évolutive** : Les plantes changent d'apparence (emoji) en fonction de leur stade de développement
- **Récolte à Maturité** : Les plantes peuvent être récoltées une fois arrivées à maturité

### 2. Indicateur Visuel de Santé des Plantes

- **Représentation des Plantes en Mauvaise Santé** : Les plantes dont la santé est inférieure à 50% sont représentées par un emoji de feuille morte (🍂)
- **Retour à la Normale** : L'apparence redevient standard quand la santé dépasse 50%

### 3. Système de Suggestions d'Actions

- **Conseils Contextuels** : En fonction de l'état de la plante et des conditions météorologiques, le jeu suggère des actions pour améliorer la santé des plantes
- **Suggestions Intelligentes** incluant :
  - Arrosage quand l'hydratation est faible
  - Désherbage et traitement pour améliorer la santé
  - Installation d'équipements de protection adaptés aux conditions actuelles (serre en hiver, pare-soleil en canicule)
- **Numérotation Claire** : Les suggestions correspondent directement aux numéros des actions dans le menu

## Implémentation Technique

### Classe CycleDeVie

Nouvelle classe qui gère les étapes de croissance spécifiques pour chaque type de plante, définissant :
- Les durées minimales par étape
- Les hauteurs minimale et maximale par étape
- L'emoji représentatif pour chaque stade
- Si la plante peut être récoltée à ce stade

### Modifications du SimulateurPlante

La méthode `CalculerEtatPlante` a été mise à jour pour utiliser le cycle de vie spécifique à chaque plante.

### Modifications de la Classe Parcelle

- Ajout d'une méthode `ObtenirSuggestions()` qui analyse l'état de la plante pour proposer des actions pertinentes
- Mise à jour de `ObtenirEmojiPlante()` pour refléter l'état de santé et utiliser les emojis du cycle de vie

### Intégration avec la Classe Temps

- Référence statique à l'instance de Temps pour accéder aux conditions météorologiques actuelles
- Initialisation de cette référence dans le constructeur de Temps

## Utilisation

Les plantes évoluent maintenant naturellement au fil des semaines. Vous pouvez surveiller leur croissance et intervenir au bon moment pour maximiser leur santé et leur rendement. Suivez les suggestions d'actions pour maintenir vos plantes en bonne santé et les conduire jusqu'à maturité.
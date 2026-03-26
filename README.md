# Projet Fil-Rouge — Weather Terrarium VR

Projet réalisé dans le cadre du cours, en sous-équipe (1 personne).

## Concept
Une serre virtuelle en réalité virtuelle dont l'environnement change en fonction de la météo.
L'utilisateur peut interagir avec les plantes pour obtenir faire apparaître un menu caché qui permet de changer la météo, et naviguer dans la scène en RV.

## États météo
| État | Ambiance | Effets |
|------|----------|--------|
| ☀️ Ensoleillé | Lumière chaude jaune | Sons d'oiseaux, plantes qui poussent |
| 🌧️ Pluvieux | Lumière bleue froide | Particules de pluie, plantes normales |
| ❄️ Neigeux | Lumière blanche froide | Particules de neige, neige sur le banc, plantes qui rétrécissent |
| ⛈️ Orageux | Lumière violette | Pluie intense, éclairs, plantes qui se recroquevillent |

## Fonctionnalités implémentées
- Scène VR fonctionnelle avec XR Interaction Toolkit (Unity 6)
- 4 états météo contrôlés par un seul script (WeatherManager)
- Particules de pluie et de neige
- Sons ambiants spatialisés par état météo
- Lumière dynamique (DomeLamp) dont la couleur change selon la météo
- Effets d'éclairs aléatoires pendant l'orage
- Changement de couleur du skybox selon la météo
- Interactions VR : attraper une plante affiche un panneau UI flottant avec les infos météo
- Menu flottant avec 4 boutons pour changer la météo en direct
- Déplacements : mouvement continu + téléportation

## Comment tester
1. Ouvrir le projet dans Unity 6
2. Ouvrir la scène `weather`
3. Appuyer sur Play
4. Utiliser le menu flottant pour changer les états météo
5. Attraper une plante pour voir les infos météo apparaître

## Contrôles (XR Simulator)
- `W A S D` — se déplacer
- `Tab` — basculer entre la caméra et le contrôle des mains
- `G` — attraper un objet
- `1 2 3 4` — changer la météo au clavier (mode debug)
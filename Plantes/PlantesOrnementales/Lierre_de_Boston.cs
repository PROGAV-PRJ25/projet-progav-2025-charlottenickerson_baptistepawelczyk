public class Lierre_de_Boston:PlanteOrnementale {

    public Lierre_de_Boston(Terrain terrain) : base(terrain) {
        Nom ="Lierre de Boston";
        Type = "Vivace";
        Saisons = new List<string> {"Printemps", "Été", "Automne"};
        TerrainPrefere = "Forêt";
        Espacement = 1;
        VitesseCroissance = 0.133;
        BesoinsEnEau = 2;
        BesoinsEnLuminosite = "Luminosité moyenne";
        TemperaturesPreferees = new List<int> {10, 25};
        MaladiesPossibles = new List<string> {"oïdium", "taches foliaires"};
        ProbabilitesMaladies = new List<int> {15, 10};
        EsperanceDeVie = 2.5;
        Rendement = 3;
        EtatPlante ="Germination";
    }
}
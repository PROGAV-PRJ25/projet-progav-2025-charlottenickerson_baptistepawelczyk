public abstract class PlanteAromatique:Plante {

    public PlanteAromatique(Terrain terrain) : base(terrain) {
        EstComestible = false;
    }
}
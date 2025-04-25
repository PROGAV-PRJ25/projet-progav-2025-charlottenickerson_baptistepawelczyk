public abstract class PlanteComestible:Plante {

    public PlanteComestible(Terrain terrain) : base(terrain) {
        EstComestible = true;
    }
}
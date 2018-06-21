using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    public Level level;
    public LevelSFX SFX;
    public Transform Player;
    public VoidEvent OnGenerated;
    //Objects
    public GameObject Solid;
    //NPCs
    public GameObject BigMouthMan;
    public GameObject DullMan;
    public GameObject HampbackMan;
    public GameObject Boss;
    //Special Pick-ups
    public GameObject HealthPickup0;
    public GameObject FuelPickup0;
    public GameObject BoostPickup0;
    public GameObject ScorePickup0;

    public GameObject LifePickup;
    //Intractable
    public GameObject Flag;
    public GameObject Finish;

    //Map
    public BlockId[][] Map;

    private ScriptEngine engine;
    private RegionGenerator[] functions;

    public void Load()
    {
        Load(Constants.ScriptsPath);
    }
    public void Load(string Path)
    {
        engine = Python.CreateEngine();
        var functions = new List<RegionGenerator>();
        var files = Directory.GetFiles(Path, "*.py", SearchOption.AllDirectories);
        foreach (string file in files)
        {
            try
            {
                functions.Add(new RegionGenerator(engine, File.ReadAllText(file)));
            }catch
            {
                Debug.Log("Error while loading script " + file);
            }
        }
        this.functions = functions.ToArray();
    }
    public void Generate(int Seed,int Level,int Regions) 
    {
        transform.ClearChildrenImmediately();
        if (engine == null) Load();
        {
            System.Random random = new System.Random(Seed);
            List<BlockId[]> map = new List<BlockId[]>();
            int height = 0;
            List<RegionGenerator> validGenerators = new List<RegionGenerator>();
            //<Generate Start>
            var start_unit = new BlockId[] { BlockId.Solid };
            for(int i = 0; i< 20; i++)
            {
                map.Add(start_unit);
            }
            //<End>
            for (int region_id=0; region_id < Regions; region_id++)
            {
                try
                {
                    int seed = random.Next();
                    float max = 0;
                    foreach (RegionGenerator function in functions)
                    {
                        if (function.CanAttach(height))
                        {
                            validGenerators.Add(function);
                            function.Init(seed);
                            max += function.Probability(Level);
                        }
                    }
                    if (validGenerators.Count == 0) break;
                    float point = (float)random.NextDouble() * max;
                    RegionGenerator f = null;
                    foreach (RegionGenerator function in validGenerators)
                    {
                        point -= function.Probability(Level);
                        if (point <= 0)
                        {
                            f = function;
                            break;
                        }
                    }
                    validGenerators.Clear();
                    PythonTuple output = f.Generate(Level, height);
                    List region = (List)output[0];
                    height = (int)output[1];
                    List<BlockId> l = new List<BlockId>();
                    foreach (List column in region)
                    {
                        foreach (int obj in column)
                        {
                            l.Add((BlockId)obj);
                        }
                        map.Add(l.ToArray());
                        l.Clear();
                    }
                }
                catch { }
            }
            //<Generate End>
            BlockId[] end_unit = new BlockId[height+1];
            BlockId[] finish_unit = new BlockId[height + 4];
            for (int i = 0; i < height; i++)
            {
                end_unit[i] = BlockId.Space;
                finish_unit[i] = BlockId.Space;
            }
            end_unit[height] = BlockId.Solid;
            finish_unit[height] = BlockId.Solid;
            finish_unit[height + 1] = BlockId.Space;
            finish_unit[height + 2] = BlockId.Space;
            finish_unit[height + 3] = BlockId.Finish;
            for (int i = 0; i < 30; i++)
            {
                if (i == 15)
                {
                    map.Add(finish_unit);
                }
                else
                {
                    map.Add(end_unit);
                }
            }
            //<End>
            Map = map.ToArray();
            Write(Map);
        }
        OnGenerated.Invoke();
    }
    void Write(BlockId[][] Map)
    {
        for(int x = 0; x < Map.Length; x++)
        {
            for(int y = 0; y < Map[x].Length; y++)
            {
                Create(Map[x][y], x, y);
            }
        }
    }
    void Create(BlockId obj,int x,int y)
    {
        if (obj == BlockId.Solid)
        {
            GameObject instance = Instantiate(Solid, transform);
            instance.transform.localPosition = new Vector2(x, y);
        }

        else if (obj == BlockId.BigMouthMan)
        {
            GameObject instance = Instantiate(BigMouthMan, transform);
            instance.transform.localPosition = new Vector2(x, y);
            instance.GetComponent<EnemyMovement>().Target = Player;
            instance.GetComponent<EnemyAttackness>().TargetCollider = Player.GetComponent<CapsuleCollider2D>();
            Health health = instance.GetComponent<Health>();
            health.OnDied.AddListener((damage) => SFX.Play(1));
        }
        else if (obj == BlockId.DullMan)
        {
            GameObject instance = Instantiate(DullMan, transform);
            instance.transform.localPosition = new Vector2(x, y);
            instance.GetComponent<EnemyMovement>().Target = Player;
            instance.GetComponent<EnemyAttackness>().TargetCollider = Player.GetComponent<CapsuleCollider2D>();
            Health health = instance.GetComponent<Health>();
            health.OnDied.AddListener((damage) => SFX.Play(1));
        }
        else if (obj == BlockId.HampbackMan)
        {
            GameObject instance = Instantiate(HampbackMan, transform);
            instance.transform.localPosition = new Vector2(x, y);
            instance.GetComponent<EnemyMovement>().Target = Player;
            instance.GetComponent<EnemyAttackness>().TargetCollider = Player.GetComponent<CapsuleCollider2D>();
            Health health = instance.GetComponent<Health>();
            health.OnDied.AddListener((damage) => SFX.Play(1));
        }
        else if (obj == BlockId.Boss)
        {
            GameObject instance = Instantiate(Boss, transform);
            instance.transform.localPosition = new Vector2(x, y);
            instance.GetComponent<EnemyMovement>().Target = Player;
            instance.GetComponent<EnemyAttackness>().TargetCollider = Player.GetComponent<CapsuleCollider2D>();
            Health health = instance.GetComponent<Health>();
            health.OnDied.AddListener((damage)=> SFX.Play(1));
        }

        else if (obj == BlockId.Health0)
        {
            GameObject instance = Instantiate(HealthPickup0, transform);
            instance.transform.localPosition = new Vector2(x, y);
            var Pickup = instance.GetComponent<PickUp>();
            Pickup.Player = Player.GetComponent<Collider2D>();
            Pickup.OnPickUp.AddListener(() =>
            {
                level.Heal(10);
                level.AddScores(50);
                SFX.Play(2);
            });
        }
        else if (obj == BlockId.Fuel0)
        {
            GameObject instance = Instantiate(FuelPickup0, transform);
            instance.transform.localPosition = new Vector2(x, y);
            var Pickup = instance.GetComponent<PickUp>();
            Pickup.Player = Player.GetComponent<Collider2D>();
            Pickup.OnPickUp.AddListener(() =>
            {
                level.Refuel(10);
                level.AddScores(50);
                SFX.Play(2);
            });
        }
        else if (obj == BlockId.Boost0)
        {
            GameObject instance = Instantiate(BoostPickup0, transform);
            instance.transform.localPosition = new Vector2(x, y);
            var Pickup = instance.GetComponent<PickUp>();
            Pickup.Player = Player.GetComponent<Collider2D>();
            Pickup.OnPickUp.AddListener(() =>
            {
                level.Reboost(20);
                level.AddScores(50);
                SFX.Play(2);
            });
        }
        else if (obj == BlockId.Score0)
        {
            GameObject instance = Instantiate(ScorePickup0, transform);
            instance.transform.localPosition = new Vector2(x, y);
            var Pickup = instance.GetComponent<PickUp>();
            Pickup.Player = Player.GetComponent<Collider2D>();
            Pickup.OnPickUp.AddListener(() =>
            {
                level.AddScores(100);
                SFX.Play(2);
            });
        }

        else if (obj == BlockId.Life)
        {
            GameObject instance = Instantiate(LifePickup, transform);
            instance.transform.localPosition = new Vector2(x, y);
            var Pickup = instance.GetComponent<PickUp>();
            Pickup.Player = Player.GetComponent<Collider2D>();
            Pickup.OnPickUp.AddListener(() =>
            {
                level.AddLife();
                SFX.Play(2);
            });
        }

        else if (obj == BlockId.Checkpoint)
        {
            GameObject instance = Instantiate(Flag, transform);
            instance.transform.localPosition = new Vector2(x, y);
            Health health = instance.GetComponent<Health>();
            health.OnTakeDamage.AddListener((damage) => level.SaveCheckpoint());
        }
        else if (obj == BlockId.Finish)
        {
            GameObject instance = Instantiate(Finish, transform);
            instance.transform.localPosition = new Vector2(x, y);
            var Pickup = instance.GetComponent<PickUp>();
            Pickup.Player = Player.GetComponent<Collider2D>();
            Pickup.OnPickUp.AddListener(() =>
            {
                level.Win();
            });
        }
    }
}
public class RegionGenerator
{
    public ScriptEngine Engine
    {
        get
        {
            return engine;
        }
    }
    public ScriptScope Scope
    {
        get
        {
            return scope;
        }
    }

    private ScriptEngine engine;
    private ScriptScope scope;
    private ScriptSource source;
    private Func<int, object> init;
    private Func<int, float> probability;
    private Func<int, bool> canAttach;
    private Func<int, int, PythonTuple> generate;

    public void Init(int Seed)
    {
        init.Invoke(Seed);
    }
    public float Probability(int Level)
    {
        return probability.Invoke(Level);
    }
    public bool CanAttach(int Height)
    {
        return canAttach.Invoke(Height);
    }
    public PythonTuple Generate(int Level, int Height)
    {
        return generate.Invoke(Level, Height);
    }

    public RegionGenerator(ScriptEngine engine, string script)
    {
        this.engine = engine;
        scope = engine.CreateScope();
        source = engine.CreateScriptSourceFromString(script);
        source.Execute(scope);
        init = scope.GetVariable<Func<int, object>>("Init");
        probability = scope.GetVariable<Func<int, float>>("Probability");
        canAttach = scope.GetVariable<Func<int, bool>>("CanAttach");
        generate = scope.GetVariable<Func<int, int, PythonTuple>>("Generate");
    }
}
public enum BlockId : int
{
    Space=0,
    Solid=1,

    BigMouthMan = 10,
    DullMan = 11,
    HampbackMan = 12,
    Boss = 13,

    Health0 = 20,
    Fuel0 = 21,
    Boost0 = 22,
    Score0 = 23,

    Life = 29,

    Checkpoint = 40,
    Finish = 41
}
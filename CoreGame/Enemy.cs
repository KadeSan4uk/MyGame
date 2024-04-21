using System.Drawing;

namespace MyGame.CoreGame
{
    public class Enemy : IBarDraw
    {
        public event Action? DiedEventEnemy;

        private const int Health = 300;
        private const int Damage = 50;
        private const int Level = 1;
        private int _health;
        private int _maxHealth;
        private int _damage;
        private int _level;

        private bool _isAlive => _health > 0;
        public int DieExperience => 1;
        public string Name { get; set; }


        private Logger _log;
        private Random _random = new();

        public Enemy(Logger log)
        {
            Name = SetName().ToString();
            _log = log;
            _health = Health;
            _maxHealth = _health;
            _damage = Damage;
            _level = Level;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;

            if (_isAlive)
            {
                if (damage != 0)
                    _log.AddLog($" Игрок нанес {damage} урона");
            }
            else
            {
                _log.AddLog($" Игрок нанес {damage} урона, враг погиб.");
                DiedEventEnemy?.Invoke();
            }
        }

        public bool TryHit(out int damage)
        {
            int chance = _random.Next(0, 100);

            if (chance > 20)
            {
                damage = _damage;
                return true;
            }

            _log.AddLog($" Враг промахнулся");
            damage = 0;
            return false;
        }

        public void HealthStatus()
        {
            Console.WriteLine($"  {Name} уровень: {_level}");
            Console.WriteLine($"\n");
        }

        public void GiveHealthForBars(ref int health, ref int MaxHealt)
        {
            health = _health;
            MaxHealt = _maxHealth;
        }

        public enum NameEnemy
        {
            Гримгор,
            Злоборог,
            Чернопасть,
            Кровожад,
            Теневик,
            Ужасоглаз,
            Костогрыз,
            Ядоклык,
            Мракогрив,
            Жуткокрыл,
            Грозозуб,
            Пепелопасть,
            Камнедых,
            Моргул,
            Пожиратель,
            Чернолап,
            Холодогрив,
            Гнилозуб,
            Вурмангл,
            Кровокрыл,
            Плотоедка,
            Огнежив,
            Громопасть,
            Ловец_кошмаров,
            Темный_коготь,
            Костегрыз,
            Ядокрыл,
            Кровавый_вихрь,
            Пепелокрыл,
            Хищный_песчанник,
            Мрачногрив,
            Вечнозуб,
            Камнекоготь,
            Мертвый_глаз,
            Черновихрь,
            Костеразрыв,
            Огненная_горгулья,
            Жутколап,
            Теневой_зуб,
            Лютокрыл,
            Гнилопасть,
            Камнежал,
            Мракозуб,
            Черносокол,
            Холодный_коготь,
            Плотожор,
            Вурмакрыл,
            Кровавая_мгла,
            Огнелап,
            Громокры,
            Трескун,
            Мракодых,
            Кровавый_шрам,
            Чернозев,
            Костевзор,
            Пепелобор,
            Ядоскверн,
            Холоднострах,
            Зловонное_горло,
            Огненная_пасть,
            Гнилозлоб,
            Молниеносец,
            Жуткий_укус,
            Темная_тень,
            Костеползун,
            Мертвый_голос,
            Подземная_могила,
            Кровавый_клык,
            Чумная_ярость,
            Ледяной_вой,
            Пепельный_зуб,
            Живогрив,
            Тьма,
            Камнеморг,
            Огненный_гнев,
            Мракобор,
            Чернозло,
            Вурмакоготь,
            Хищный_жнец,
            Ядовитая_тьма,
            Гнилой_укус,
            Грозовая_мгла,
            Теневое_сердце,
            Пожиратель_миров,
            Кровавый_вой,
            Огненный_костер,
            Чудовищный_коготь,
            Жуткий_голос,
            Темный_пожиратель,
            Костедых,
            Мракоплет,
            Лютый_огонь,
            Пепельный_пожиратель,
            Ядовитый_вихрь,
            Чумная_гниль,
            Гримозуб,
            Теневой_укус,
            Жуткий_пожиратель,
            Кровавый_кошмар,
            Огненный_темный
        }

        public NameEnemy SetName()
        {
            Random random = new Random();
            int index = random.Next(Enum.GetNames(typeof(NameEnemy)).Length);

            return (NameEnemy)index;
        }

    }
}
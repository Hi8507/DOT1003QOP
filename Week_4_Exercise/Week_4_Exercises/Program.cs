// 3.0

//What if we need to add 5 more resources (Wood, Iron, etc.)?
public Text goldText;
public Text crystalText;
public Text foodText;
public Text woodText;
public Text ironText;
void Update(Text textElement, int amount) {
textElement.text = "Amount: " + amount.ToString();
}

//What if the audio logic changes (e.g., adding pitch variations)?
void PlaySound(AudioClip clip) {
AudioSource audio = GetComponent<AudioSource>();
audio.clip = clip;
audio.pitch = Random.Range(0.9f, 1.1f); // pitch variation
audio.Play();
}
void Jump() {
PlaySound(jumpSound);
rb.velocity = Vector2.up * jumpForce;
}
void Shoot() {
PlaySound(shootSound);
Instantiate(bullet);
}

//What if we add an invulnerability mechanic later?
public bool isInvulnerable = false;
void ApplyDamage(int amount) {
if (!isInvulnerable) {
health -= amount;
if (health < 0)
health = 0;
Debug.Log("Health: " + health);
}
}
void TakePhysicalDamage(int amount) {
ApplyDamage(amount);
}
void TakeMagicDamage(int amount) {
ApplyDamage(amount);
}

//What if the spawn effect changes?
public GameObject goblinPrefab;
public GameObject orcPrefab;
void SpawnEnemy(GameObject enemyPrefab) {
Vector3 spawnPos = transform.position + new Vector3(0, 1, 0);
Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
PlaySpawnParticle(spawnPos);
}
void SpawnGoblin() {
SpawnEnemy(goblinPrefab);
}
void SpawnOrc() {
SpawnEnemy(orcPrefab);
}

//What if the map size increases?
float mapLimitRight = 100f;
float mapLimitLeft = -100f;
void MoveRight() {
if (transform.position.x < mapLimitRight) {
transform.Translate(Vector3.right * speed * Time.deltaTime);
}
}
void MoveLeft() {
if (transform.position.x > mapLimitLeft) {
transform.Translate(Vector3.left * speed * Time.deltaTime);
}
}

// 3.1

//You are making a simple platformer where a potion just heals 10 HP. What if?
public class HealthPotion {
public int healAmount = 10;
public void Consume(Player player) {
player.Heal(healAmount);
}
}

//You are making a simple Pac-Man clone where the player just collects dots for points.
public class CollectibleDot {
public int pointValue = 10;
public void Collect(Player player) {
player.AddScore(pointValue);
Debug.Log(“+ ” + pointValue.ToString);
}
}

// Code 3 -> You are making a Space Invaders clone where the ship ONLY moves left and
right. What if?
public class Spaceship {
public float moveSpeed = 5f;
public void Move(Vector3 direction) {
transform.Translate(direction * moveSpeed * Time.deltaTime);
}
}

//You are making a hyper-casual mobile game (1 tap to jump). What if?
public class PlayerStats
{
public float jumpForce = 5f;
public void IncreaseJumpForce(float amount)
{
jumpForce += amount;
}
}

//The game only has ONE gun that shoots normal bullets. What if?
public interface IWeapon
{
void Fire();
void Reload();
}
public class Pistol : IWeapon
{
public void Fire()
{
// Shoot bullet
}
public void Reload()
{
// Reload ammo
}
}

// 3.2

//What if someone reads this boolean check?
public bool IsPlayerDead() {
if (health <= 0) {
return true;
}
}

//What if we need to initialize a list of starting levels?
List<int> startingLevels = new List<int> { 1, 2, 3 };
void Start()
{
}

//What if we need to check the enemy type?
void CheckEnemy(string enemyType)
{
if (enemyType == "Goblin" || enemyType == "Orc" || enemyType == "Troll")
{
Attack();
}
else
{
RunAway();
} }

//What if we need a simple 5-second cooldown timer?
float timer = 0f;
bool isCooldown = true;
void Update() {
if (isCooldown || timer <= 5f) {
timer += Time.deltaTime;
else {
isCooldown = false;
}
}
}

//What if we need to find the highest score between two players?
int GetHighestScore(int score1, int score2) {
if (score1 < score 2){
return score2;
}
else{
return score1;
}
}
extends CharacterBody2D

const SPEED = 200
const JUMP_VELOCITY = -400

# Importa o módulo ProjectSettings para acessar as configurações do projeto
var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")


# criamos uma variável para armazenar a referência ao AnimatedSprite2D
var animatedSprite2D: AnimatedSprite2D

func _ready():
	# desta maneira, o AnimatedSprite2D é carregado
	# e podemos acessar suas propriedades e métodos
	animatedSprite2D = get_node("AnimatedSprite2D")



func _physics_process(delta):
	velocity.y += gravity * delta

	var direction = Input.get_action_strength("ui_right") - Input.get_action_strength("ui_left")
	velocity.x = direction * SPEED


# ANIMATIONS
	if direction != 0:
		animatedSprite2D.play("run")
		animatedSprite2D.flip_h = direction < 0
	elif not is_on_floor():
		animatedSprite2D.play("jump")
	else:
		animatedSprite2D.play("idle")

	# Verifica se o personagem está no chão e se a tecla de pulo foi pressionada
	if is_on_floor() and Input.is_action_just_pressed("ui_up"):
		
		velocity.y = JUMP_VELOCITY

	# Verifica se o personagem está no ar e se a tecla de pulo foi pressionada
	move_and_slide()

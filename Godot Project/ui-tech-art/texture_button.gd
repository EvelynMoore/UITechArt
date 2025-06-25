extends TextureButton

@export var dissolve_duration: float = 1.0
@export var distanceToMove: float = 20.0;
@export var duration: float = 0.1;
@export var distToTeleport: float = 300;
@export var waitBeforeInitMove: float = 0.0;
@export var initMoveDuration= 0.5;
var original_position: Vector2
var initTween
var material_ref: ShaderMaterial
var tween

func _ready():
	material_ref = self.material as ShaderMaterial
	# Duplicate the shared material so this button gets a unique instance
	if self.material is ShaderMaterial:
		material_ref = self.material.duplicate()
		self.material = material_ref
	else:
		push_error("This TextureButton does not have a ShaderMaterial assigned!")
		return
		
	pressed.connect(_on_button_pressed)
	original_position = position
	position.x -= distToTeleport;
	initTween = get_tree().create_tween()
	initTween.tween_interval(waitBeforeInitMove)
	initTween.tween_property(self, "position", original_position, initMoveDuration)\
		.set_trans(Tween.TRANS_SINE)\
		.set_ease(Tween.EASE_IN_OUT)
		
func _on_mouse_entered():
	if initTween: initTween.kill()
	initTween = get_tree().create_tween()
	initTween.tween_property(self, "position", original_position + Vector2(distanceToMove, 0), duration)\
		.set_trans(Tween.TRANS_SINE)\
		.set_ease(Tween.EASE_IN_OUT)
		
func _on_mouse_exited():
	if initTween: initTween.kill()
	initTween = get_tree().create_tween()
	initTween.tween_property(self, "position", original_position, duration)\
		.set_trans(Tween.TRANS_SINE)\
		.set_ease(Tween.EASE_IN_OUT)

func _on_button_pressed():
	# Reset dissolve_amount to the visible state
	material_ref.set_shader_parameter("dissolve_amount", -0.9)

	if tween:
		tween.kill()

	tween = create_tween()
	tween.tween_property(
		material_ref,
		"shader_parameter/dissolve_amount",
		1.0,
		dissolve_duration
	).set_trans(Tween.TRANS_SINE).set_ease(Tween.EASE_IN_OUT)

	tween.tween_callback(func(): self.disabled = true)

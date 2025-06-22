extends TextureButton

@export var dissolve_duration: float = 1.0

var material_ref: ShaderMaterial
var tween

func _ready():
	# Get the ShaderMaterial applied to this TextureButton
	material_ref = self.material as ShaderMaterial
	if material_ref == null:
		push_error("This TextureButton does not have a ShaderMaterial assigned!")
		return

	# Connect the pressed signal
	pressed.connect(_on_button_pressed)

func _on_button_pressed():
	# Reset dissolve_amount to the visible state
	material_ref.set_shader_parameter("dissolve_amount", -1.0)

	# Clean up any previous tween
	if tween:
		tween.kill()

	# Create a new tween
	tween = create_tween()

	# Animate dissolve_amount from -1.0 to 1.0 over dissolve_duration
	tween.tween_property(
		material_ref,
		"shader_parameter/dissolve_amount",
		1.0,
		dissolve_duration
	).set_trans(Tween.TRANS_SINE).set_ease(Tween.EASE_IN_OUT)

	# Optional: Disable button after dissolving
	tween.tween_callback(func(): self.disabled = true)

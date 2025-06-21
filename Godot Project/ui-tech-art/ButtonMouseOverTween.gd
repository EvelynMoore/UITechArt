extends Button

@export var distanceToMove: float = 20.0;
@export var duration: float = 0.1;
var original_position: Vector2
var tween

func _ready():
	original_position = position
	print(original_position)

func _on_mouse_entered():
	if tween: tween.kill()
	tween = get_tree().create_tween()
	tween.tween_property(self, "position", original_position + Vector2(distanceToMove, 0), 0.2)\
		.set_trans(Tween.TRANS_SINE)\
		.set_ease(Tween.EASE_IN_OUT)

func _on_mouse_exited():
	if tween: tween.kill()
	tween = get_tree().create_tween()
	tween.tween_property(self, "position", original_position, duration)\
		.set_trans(Tween.TRANS_SINE)\
		.set_ease(Tween.EASE_IN_OUT)

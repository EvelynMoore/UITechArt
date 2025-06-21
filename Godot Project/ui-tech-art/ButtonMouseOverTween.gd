extends Button

@onready var original_position := position
var tween: SceneTreeTween

func _on_mouse_entered():
	if tween: tween.kill()  # Cancel any running tweens
	tween = get_tree().create_tween()
	tween.tween_property(self, "position", original_position + Vector2(10, 0), 0.2)\
		.set_trans(Tween.TRANS_SINE)\
		.set_ease(Tween.EASE_OUT)

func _on_mouse_exited():
	if tween: tween.kill()
	tween = get_tree().create_tween()
	tween.tween_property(self, "position", original_position, 0.2)\
		.set_trans(Tween.TRANS_SINE)\
		.set_ease(Tween.EASE_OUT)

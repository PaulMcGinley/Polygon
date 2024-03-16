Custom polygon shape created from points, the last point must be the same as the first point to close the shape.

Done:
 - Custom Shape
 - Point in shape detection
 - Rotation
 - Auto detect origin

TODO:
 - Add scaling
 - Add movement

Building the shape:
Populate the Points list ```List<Vector2> Points;``` with a 2D point map of the shapes corners,
for example:
Square:
```
pointShape.AddPoint(new Vector2(100, 100)); // First Point
pointShape.AddPoint(new Vector2(200, 100));
pointShape.AddPoint(new Vector2(200, 200));
pointShape.AddPoint(new Vector2(100, 200));
pointShape.AddPoint(new Vector2(100, 100)); // Close the shape by making the last point the same as the first point
```

Star:
```
pointShape.AddPoint(new Vector2(750, 50));
pointShape.AddPoint(new Vector2(800, 150));
pointShape.AddPoint(new Vector2(900, 150));
pointShape.AddPoint(new Vector2(825, 200));
pointShape.AddPoint(new Vector2(850, 300));
pointShape.AddPoint(new Vector2(750, 250));
pointShape.AddPoint(new Vector2(650, 300));
pointShape.AddPoint(new Vector2(675, 200));
pointShape.AddPoint(new Vector2(600, 150));
pointShape.AddPoint(new Vector2(700, 150));
pointShape.AddPoint(new Vector2(750, 50));
```

Calculate the new position of a point as it is being rotated:
Knowing the points current location, the origin or 'center' of the custom shape as well as the rotation amount
we can do some simple math to calculate the new position of the point.
```
private Vector2 RotatePoint(Vector2 vector2, Vector2 origin, float rotation) {

  var x = (float)(origin.X + (vector2.X - origin.X) * Math.Cos(rotation) - (vector2.Y - origin.Y) * Math.Sin(rotation));
  var y = (float)(origin.Y + (vector2.X - origin.X) * Math.Sin(rotation) + (vector2.Y - origin.Y) * Math.Cos(rotation));
  return new Vector2(x, y);
}
```

Calculating the origin or centermost point of the shape:
We track all the x & y positions, adding them together and diving by the amount of points minus one as the last point is
soley used to close the shape.
This gives us an average for each x and y point which so far is working for the shapes I have tested it with.
```
private Vector2 CalculateCentermostPoint() {

  float x = 0, y = 0;

  for (int i = 0; i < Points.Count-1; i++) {

    x += Points[i].X;
    y += Points[i].Y;
  }

  return new Vector2(x / (Points.Count-1), y / (Points.Count-1));
}
```

Calculating if a given point is in the custom shape:
Using variabled i & j to track the start and end of a line, we use a ray casting algorythem to determine if the point
is indeed located within the custom shape.
```
public bool PointIsInRotatedShape(Vector2 point) {

  List<Vector2> rotatedPoints = new List<Vector2>();

  for (int r = 0; r < Points.Count; r++)
    rotatedPoints.Add(RotatePoint(Points[r], origin, rotation));

  int i, j;
  bool c = false;
  for (i = 0, j = rotatedPoints.Count - 1; i < rotatedPoints.Count; j = i++) {

    if (((rotatedPoints[i].Y > point.Y) != (rotatedPoints[j].Y > point.Y)) &&
       (point.X < (rotatedPoints[j].X - rotatedPoints[i].X) * (point.Y - rotatedPoints[i].Y) / (rotatedPoints[j].Y - rotatedPoints[i].Y) + rotatedPoints[i].X))
      c = !c;
  }

  return c;
}
```

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Polygon;

public class PointShape {

    Texture2D texLine;
    List<Vector2> Points;
    float rotation = 1;
    Vector2 origin;

    BoundingBox bBox = new BoundingBox();

    public PointShape(GraphicsDevice device) {

        Points = new List<Vector2>();
        
        // Create a 1x1 white texture
        texLine = new Texture2D(device, 1, 1);
        texLine.SetData(new[] { Color.White });     

        BoundingBox bBox = new(new Vector3(420, 420, 0), new Vector3(400, 400, 0));
    }

    public void AddPoint(Vector2 point) => Points.Add(point);    

    public void Draw(SpriteBatch spriteBatch) {

        spriteBatch.Draw(texLine, origin, null, PointIsInRotatedShape(Mouse.GetState().Position.ToVector2()) ? Color.Red : Color.White, rotation, new Vector2(0.5f, 0.5f), new Vector2(95, 95), SpriteEffects.None, 0);

        for (int i = 0; i < Points.Count - 1; i++) {

            Vector2 newPointBasedOnRotation = RotatePoint(Points[i], origin, rotation);
            Vector2 newPointBasedOnRotation2 = RotatePoint(Points[i + 1], origin, rotation);
            
            DrawLine(newPointBasedOnRotation, newPointBasedOnRotation2, spriteBatch);

            // draw the bounding box
             spriteBatch.Draw(texLine, new Vector2(bBox.Min.X, bBox.Min.Y), null, Color.Pink, 0, new Vector2(0.5f, 0.5f), new Vector2(10, 10), SpriteEffects.None, 0);
        }
    }

    // This doesn't account for rotation
    // public bool PointIsInShape(Vector2 point) {
    //
    //     int i, j;
    //     bool c = false;
    //     for (i = 0, j = Points.Count - 1; i < Points.Count; j = i++) {
    //
    //         if (((Points[i].Y > point.Y) != (Points[j].Y > point.Y)) &&
    //             (point.X < (Points[j].X - Points[i].X) * (point.Y - Points[i].Y) / (Points[j].Y - Points[i].Y) + Points[i].X))
    //             c = !c;
    //     }
    //     return c;
    // }

    /// <summary>
    /// The provided C# function, PointIsInRotatedShape, is designed to determine whether a given point lies within a shape 
    /// constructed by a set of points that have been rotated. This function is crucial for geometric calculations and can 
    /// be used in various applications where point-inclusion tests are required.
    /// 
    /// Rotated Points List: The function first creates a list of rotated points based on the original points of the shape. 
    /// These rotated points are calculated by applying a rotation transformation to each point around a specified origin.
    /// 
    /// Point-Inclusion Test: The function then performs a point-inclusion test by iterating through the rotated points and 
    /// checking if the given point lies inside the shape. It uses a ray-casting algorithm to determine the inclusion based 
    /// on the number of intersections between the shape boundary and a horizontal ray extending from the given point.
    /// 
    /// https://en.wikipedia.org/wiki/Ray_casting
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
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

    private Vector2 RotatePoint(Vector2 vector2, Vector2 origin, float rotation) {

        var x = (float)(origin.X + (vector2.X - origin.X) * Math.Cos(rotation) - (vector2.Y - origin.Y) * Math.Sin(rotation)); //
        var y = (float)(origin.Y + (vector2.X - origin.X) * Math.Sin(rotation) + (vector2.Y - origin.Y) * Math.Cos(rotation));
        return new Vector2(x, y);
    }

    public void Update(GameTime gameTime) {

        rotation += 0.01f;
        origin = CalculateCentermostPoint();
    }

    private Vector2 CalculateCentermostPoint() {

        float x = 0, y = 0;

        for (int i = 0; i < Points.Count-1; i++) {

            x += Points[i].X;
            y += Points[i].Y;
        }

        return new Vector2(x / (Points.Count-1), y / (Points.Count-1));
    }

    void DrawLine(Vector2 start, Vector2 end, SpriteBatch spriteBatch) {

        var distance = Vector2.Distance(start, end);
        var angle = (float)Math.Atan2(end.Y - start.Y, end.X - start.X);
        var scale = new Vector2(distance, 1);
        var origin = new Vector2(0, 0.5f);
        var color = Color.White;

        spriteBatch.Draw(texLine, start, null, color, angle, origin, scale, SpriteEffects.None, 0);
    }

    public bool Intersects(BoundingSphere sphere) {
            
            for (int i = 0; i < Points.Count - 1; i++) {
    
                Vector2 newPointBasedOnRotation = RotatePoint(Points[i], origin, rotation);
                Vector2 newPointBasedOnRotation2 = RotatePoint(Points[i + 1], origin, rotation);
    
                if (LineIntersectsCircle(newPointBasedOnRotation, newPointBasedOnRotation2, sphere.Center, sphere.Radius))
                    return true;
            }
            return false;
    }

    private bool LineIntersectsCircle(Vector2 newPointBasedOnRotation, Vector2 newPointBasedOnRotation2, Vector3 center, float radius)
    {
        float segmentLength = Vector2.Distance(newPointBasedOnRotation, newPointBasedOnRotation2);
        float distanceToSegmentStart = Vector2.Distance(origin, newPointBasedOnRotation);
        float distanceToSegmentEnd = Vector2.Distance(origin, newPointBasedOnRotation2);

        if (distanceToSegmentStart < radius || distanceToSegmentEnd < radius)
            return true;

        float closestPoint = (float)((Math.Abs((origin - newPointBasedOnRotation).LengthSquared() - (origin - newPointBasedOnRotation2).LengthSquared() + segmentLength * segmentLength)) / (2 * segmentLength));
        Vector2 closestPointVector = newPointBasedOnRotation + (newPointBasedOnRotation2 - newPointBasedOnRotation) * (closestPoint / segmentLength);
        float distanceToClosestPoint = Vector2.Distance(origin, closestPointVector);

        return distanceToClosestPoint < radius;
    }
}
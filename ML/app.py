from flask import Flask,request,jsonify
import joblib
import numpy as np
import pandas as pd
from transform_modules import *

app=Flask(__name__)

kmeans = joblib.load('clusterer.pkl')
scaler = joblib.load('scaler_of_clusterer.pkl')

@app.route('/predict-cluster',methods=["POST"])
def predict_cluster():
    originalData=request.json

    data = {
    'BHK': originalData.get('bhk'),
    'Rent': originalData.get('rent'),
    'Size': originalData.get('size'),
    'Floor': originalData.get('floor'),
    'Area_Type': originalData.get('area_Type'),
    'Furnishing_Status': originalData.get('furnishing_Status'),
    'Tenant_Preferred': originalData.get('tenant_Preferred')
    }
    data=handle_missing(data)
    print(data)
    print(data)
    df=pd.DataFrame([data])
    df=convert_for_clustering(df,scaler)
    y=df.values
    cluster=kmeans.predict(y)

    return jsonify({
        "cluster":int(cluster[0])
    })

if __name__ == "__main__":
    app.run(debug=True)
import numpy as np
def handle_missing(dict):
    bhk_med=2
    rent_med=16000
    size_med=850

    if(dict.get('BHK')==None):
        dict['BHK']=bhk_med
    
    if(dict.get('Rent')==None):
        dict['Rent']=rent_med

    if(dict.get('Size')==None):
        dict['Size']=size_med
    
    if(dict.get('Floor')==None):
        dict['Floor']="1 out of 2"
    
    return dict



def convert_for_clustering(df,scaler):
    df['Rent']=np.log1p(df['Rent'])
    df['Size']=np.log1p(df['Size'])

    df['Area Type_Built Area'] = (df['Area_Type'] == 'Built Area').astype(int)
    df['Area Type_Carpet Area'] = (df['Area_Type'] == 'Carpet Area').astype(int)
    df['Area Type_Super Area'] = (df['Area_Type'] == 'Super Area').astype(int)

    df['Furnishing Status_Furnished']= (df['Furnishing_Status']=='Furnished').astype(int)
    df['Furnishing Status_Unfurnished']= (df['Furnishing_Status']=='Unfurnished').astype(int)
    df['Furnishing Status_Semi-Furnished']= (df['Furnishing_Status']=='Semi-Furnished').astype(int)

    df['Tenant Preferred_Bachelors/Family']=(df['Tenant_Preferred']=='Bachelors/Family').astype(int)
    df['Tenant Preferred_Bachelors']=(df['Tenant_Preferred']=='Bachelors').astype(int)
    df['Tenant Preferred_Family']=(df['Tenant_Preferred']=='Family').astype(int)

    df[['current_floor', 'total_floors']] = df['Floor'].str.extract(r'(\w+)\s*out of\s*(\d+)')
    df['current_floor'] = df['current_floor'].replace(['Ground','Basement'], 0)

    df['current_floor'] = df['current_floor'].astype(int)
    df['total_floors'] = df['total_floors'].astype(int)
    df=df.drop(['Floor','Area_Type','Furnishing_Status','Tenant_Preferred'],axis=1)

    df['floor_ratio'] = df['current_floor'] / df['total_floors']
    df.drop(['current_floor', 'total_floors'], axis=1, inplace=True)


    df[['Rent','Size','BHK']]=scaler.transform(df[['Rent','Size','BHK']])
    return df
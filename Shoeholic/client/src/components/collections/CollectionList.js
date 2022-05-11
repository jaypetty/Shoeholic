import React, { useEffect, useState, } from "react";
import { Button } from "reactstrap";
import { useParams } from "react-router-dom";
import { getUserCollectionByUserId } from "../../modules/collectionManager";
import Collection from "./Collection";

const MyCollections = () => {
    
    const [collections, setCollections] = useState([]);

    const getCollections = () => {
        getUserCollectionByUserId().then(collection => setCollections(collection));
    }

    useEffect(() => {
        getCollections();
    }, [])

    return (
        <div className="container">
            <div className="row justify-content-center">
                {collections.map((collection) => (
                    <Collection key={collection.id} collection={collection}  />
                ))}
                <Button className="btn btn-primary" href="/mycollections/newCollection">Add A New Collection</Button>
            </div>
        </div>
    );
}

export default MyCollections;
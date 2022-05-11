import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { useParams } from "react-router-dom";
import {Card,CardImg, CardText,CardTitle,Container,Button} from "reactstrap";
import { getShoeById, deleteShoe } from "../../modules/shoeManager";
import { Link } from "react-router-dom";

const ShoeDetails = () => {
    const [shoe, setShoe] = useState();
    const {id} = useParams();
    const history = useHistory();

    const getShoe = (id) => {
        getShoeById(id).then((shoe) => setShoe(shoe));
    };

    const deleteAShoe = () => {
        deleteShoe(shoe.id).then(() => history.push(`/myshoes`));
    };

    useEffect(() => {
        getShoe(id);
    }, [])
    if (shoe == null){
        return null
    };

    return (
        <Container>
            <Card>
                <CardTitle>{shoe.name} by {shoe.brand.name}</CardTitle>
                <CardText>
                    <small>{shoe.releaseDate.split("T")[0]}</small>
                </CardText>
                <CardText>
                    <small>{shoe.retailPrice}</small>
                </CardText>
                <CardText>
                    <small>{shoe.purchaseDate.split("T")[0]}</small>
                </CardText>
                <CardText>
                    <small>{shoe.title}</small>
                </CardText>
                <CardText>
                    <small>{shoe.colorWay}</small>
                </CardText>
                <CardText>
                    <>{shoe.tags.map(tag =>{
                    return  tag.name
                    }).join(', ')}</>
                </CardText>
                <Link to={`/myshoes/edit/${id}`}>
            <Button>Edit</Button>
          </Link>
          <Button  onClick={() => deleteAShoe()}>
                                    Delete
                                </Button>
            </Card>
        </Container>
    );
};

export default ShoeDetails;
import React from "react";
import { Card, CardBody, CardTitle, CardText} from "reactstrap";

const Shoe = ({shoe}) => {
    return (
        <Card>
            <CardBody>
                <CardTitle tag="h1">
                    {shoe.name}
                </CardTitle>
                <CardText>
                  {shoe.title} 
                  
                </CardText>
            </CardBody>
        </Card>
    );
};

export default Shoe;
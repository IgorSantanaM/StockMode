import React from "react";
import { ArrowUpRight, ArrowDownRight } from "lucide-react";
import { Card } from "./styles";

const StatCard = ({title, value, change, isPositive, icon}) => {
    return(
        <Card>
            <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '0.5rem' }}>
                  <h3 style={{ fontSize: '1rem', fontWeight: 500, color: '#6b7280' }}>{title}</h3>
                  {icon}
                </div>
                <div>
                  <p style={{ fontSize: '1.875rem', fontWeight: 'bold', color: '#1f2937' }}>{value}</p>
                  {change && (
                    <div style={{ display: 'flex', alignItems: 'center', fontSize: '0.875rem', marginTop: '0.25rem' }}>
                      {isPositive !== null ? (isPositive ? <ArrowUpRight style={{ height: '1rem', width: '1rem', color: '#10b981' }} /> : <ArrowDownRight style={{ height: '1rem', width: '1rem', color: '#ef4444' }} />) : null}
                      <span style={{ marginLeft: '0.25rem', color: isPositive === null ? '#6b7280' : (isPositive ? '#059669' : '#dc2626') }}>
                        {change}
                      </span>
                    </div>
                  )}
                </div>
        </Card>
    );
}

export default StatCard;
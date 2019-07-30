<?php
namespace App\Model\Table;

use Cake\ORM\Query;
use Cake\ORM\RulesChecker;
use Cake\ORM\Table;
use Cake\Validation\Validator;

/**
 * Steranking Model
 *
 * @method \App\Model\Entity\Steranking get($primaryKey, $options = [])
 * @method \App\Model\Entity\Steranking newEntity($data = null, array $options = [])
 * @method \App\Model\Entity\Steranking[] newEntities(array $data, array $options = [])
 * @method \App\Model\Entity\Steranking|bool save(\Cake\Datasource\EntityInterface $entity, $options = [])
 * @method \App\Model\Entity\Steranking saveOrFail(\Cake\Datasource\EntityInterface $entity, $options = [])
 * @method \App\Model\Entity\Steranking patchEntity(\Cake\Datasource\EntityInterface $entity, array $data, array $options = [])
 * @method \App\Model\Entity\Steranking[] patchEntities($entities, array $data, array $options = [])
 * @method \App\Model\Entity\Steranking findOrCreate($search, callable $callback = null, $options = [])
 */
class SterankingTable extends Table
{
    /**
     * Initialize method
     *
     * @param array $config The configuration for the Table.
     * @return void
     */
    public function initialize(array $config)
    {
        parent::initialize($config);

        $this->setTable('steranking');
        $this->setDisplayField('Id');
        $this->setPrimaryKey('Id');
    }

    /**
     * Default validation rules.
     *
     * @param \Cake\Validation\Validator $validator Validator instance.
     * @return \Cake\Validation\Validator
     */
    public function validationDefault(Validator $validator)
    {
        $validator
            ->integer('Id')
            ->allowEmptyString('Id', null, 'create');

        $validator
            ->scalar('Name')
            ->maxLength('Name', 20)
            ->requirePresence('Name', 'create')
            ->notEmptyString('Name');

        $validator
            ->integer('Score')
            ->requirePresence('Score', 'create')
            ->notEmptyString('Score');

        $validator
            ->dateTime('Date')
            ->notEmptyDateTime('Date');

        return $validator;
    }
}

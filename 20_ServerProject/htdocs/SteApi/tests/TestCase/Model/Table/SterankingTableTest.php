<?php
namespace App\Test\TestCase\Model\Table;

use App\Model\Table\SterankingTable;
use Cake\ORM\TableRegistry;
use Cake\TestSuite\TestCase;

/**
 * App\Model\Table\SterankingTable Test Case
 */
class SterankingTableTest extends TestCase
{
    /**
     * Test subject
     *
     * @var \App\Model\Table\SterankingTable
     */
    public $Steranking;

    /**
     * Fixtures
     *
     * @var array
     */
    public $fixtures = [
        'app.Steranking'
    ];

    /**
     * setUp method
     *
     * @return void
     */
    public function setUp()
    {
        parent::setUp();
        $config = TableRegistry::getTableLocator()->exists('Steranking') ? [] : ['className' => SterankingTable::class];
        $this->Steranking = TableRegistry::getTableLocator()->get('Steranking', $config);
    }

    /**
     * tearDown method
     *
     * @return void
     */
    public function tearDown()
    {
        unset($this->Steranking);

        parent::tearDown();
    }

    /**
     * Test initialize method
     *
     * @return void
     */
    public function testInitialize()
    {
        $this->markTestIncomplete('Not implemented yet.');
    }

    /**
     * Test validationDefault method
     *
     * @return void
     */
    public function testValidationDefault()
    {
        $this->markTestIncomplete('Not implemented yet.');
    }
}
